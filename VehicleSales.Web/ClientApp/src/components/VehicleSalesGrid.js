import React from "react";
import { AgGridReact } from "ag-grid-react";
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-alpine.css';

export default function VehicleSalesGrid(props)
{
    return (
        <div>
            <h3>Vehicle Sales Data</h3>
            <br/>
            <div className="ag-theme-alpine" style={{height: 600, width: 1300}}>
                <AgGridReact
                    rowData={props.formattedData}
                    columnDefs={props.columnDefs}>
                </AgGridReact>
            </div>
        </div>
    )
}