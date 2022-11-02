import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Card, CardBody, Button } from "reactstrap";
import { getUserIngredientById } from "../../modules/userIngredientManager";

export const UserIngredientDetails = () => {
    const navigate = useNavigate();
    const [userIngredient, setUserIngredient] = useState({})
    const [details, setDetails] = useState({
        strIngredient: '',
        strDescription: ''
    })

    const getUserIngredient = () => {
        getUserIngredientById(ingredientId).then(ui => {


        })
    }
    useEffect(() => {
        getUserIngredient()
    }, []);


    return (

        
    )
}