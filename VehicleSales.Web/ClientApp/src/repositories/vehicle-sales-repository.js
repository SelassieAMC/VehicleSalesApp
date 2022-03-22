require('dotenv').config();
const baseUrl = `${process.env.REACT_APP_API_URL}/VehicleSales`;
    
export async function getVehicleSale()
{
    return fetch(baseUrl)
        .then(response => {
            return response.json();
        })
        .catch(error => {
            throw error;
        });
}

export async function uploadVehicleSalesData(selectedFile)
{
    const formData = new FormData();
    
    formData.append('File', selectedFile);
    
    return fetch(`${baseUrl}/upload-from-csv`, 
        {
        method: 'POST',
        body: formData
        })
        .then(response => {
            if (response.status !== 200)
                throw new Error('Upload failed');
        })
        .catch(error => {
            throw error;
        });
}