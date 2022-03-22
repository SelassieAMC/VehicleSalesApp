import React, {useEffect, useState} from 'react';
import * as vehicleSalesApi from "../repositories/vehicle-sales-repository";
import { formatToCAD, groupBy } from "../helpers/helper-functions";
import UploadFile from "./UploadFile";
import VehicleSalesGrid from "./VehicleSalesGrid";


export default function Home()
{
    const [vehicleSales, setVehicleSales] = useState([]);
    const [columnDefs] = useState([
        { field: 'dealNumber' },
        { field: 'customerName' },
        { field: 'dealershipName' },
        { field: 'vehicle' },
        { field: 'price' },
        { field: 'date' }
    ]);
    
    useEffect(() => {
        getVehicleSalesData();
    },[]);
    
    function getVehicleSalesData()
    {
        vehicleSalesApi.getVehicleSale()
            .then(response => {
                setVehicleSales(response);
            })
            .catch(error => {
                console.log(error)
            });
    }
    
    function formattedData()
    {
        return vehicleSales.map((sale, index) => {
            return {...sale, price: formatToCAD(sale.price), date: new Date(sale.date).toDateString()}
        })
    }
    
    function mostOftenSoldVehicle()
    {
        const groupedSalesByVehicle = groupBy(vehicleSales, 'vehicle');
        const mostOftenSold = Object.entries(groupedSalesByVehicle)
            .sort(function(a,b){return b[1].length - a[1].length})[0];
        return `${mostOftenSold[0]} (sold ${mostOftenSold[1].length} times)`;
    }
    
    function handleSubmission(selectedFile)
    {
        vehicleSalesApi.uploadVehicleSalesData(selectedFile)
            .then(() => {
                alert("Data uploaded successfully.");
                getVehicleSalesData();
            })
            .catch(error => alert(error));
    }
    
    return (
        <div>
            <div style={{display: 'flex', justifyContent: "space-between"}}>
                {vehicleSales.length > 0 && (<div>
                    <h3>Vehicle most often sold</h3>
                    <br/>
                    <h5>{mostOftenSoldVehicle()}</h5>
                </div>)}
                <UploadFile onSubmission={(file) => handleSubmission(file)}/>
            </div>
            <hr/>
            {vehicleSales.length > 0 ? (<VehicleSalesGrid formattedData={formattedData()} columnDefs={columnDefs}/>) : (<h5>No sales data to show.</h5>)}
        </div>
    )
}