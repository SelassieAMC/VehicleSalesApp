import React, {useState} from "react";

export default function UploadFile(props)
{
    const [selectedFile, setSelectedFile] = useState(null);
    const [isFileSelected, setIsFileSelected] = useState(false);
    const inputRef = React.useRef();
    
    function onSubmission()
    {
        props.onSubmission(selectedFile);
        setIsFileSelected(false);
        setSelectedFile(null);
        inputRef.current.value = '';
    }
    
    function onFileChange(event)
    {
        setSelectedFile(event.target.files[0]);
        setIsFileSelected(true);
    }
    
    return (
        <div>
            <h5>Upload vehicle sales data</h5>
            <input type="file" name="file" className="btn btn-secondary btn-sm" onChange={onFileChange} ref={inputRef} accept='.csv'/>
            {isFileSelected && (
                <div>
                    <p>Filename: {selectedFile.name}</p>
                    <p>Filetype: {selectedFile.type}</p>
                    <p>Size in bytes: {selectedFile.size}</p>
                    <p>
                        lastModifiedDate:{' '}
                        {selectedFile.lastModifiedDate.toLocaleDateString()}
                    </p>
                </div>
            )}
            <div>
                <button className="btn btn-primary btn-sm" style={{marginTop:'10px'}} onClick={onSubmission} disabled={!isFileSelected}>Upload data</button>
            </div>
        </div>
    )
}