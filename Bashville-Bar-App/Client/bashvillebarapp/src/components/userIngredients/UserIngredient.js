import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { Card, CardBody, Input, Label } from "reactstrap";



export const UserIngredient = ({ userIngredient }) => {
    const navigate = useNavigate()

    return (
        <Card id="card">
            <CardBody>
                <h4>{`${userIngredient.ingredient.name}`}</h4>
                <Label check>

                    <Link to={`/ingredients/details/${userIngredient.ingredient.name}`}>
                        <h6>{`${userIngredient.ingredient.name}`}</h6>
                    </Link>
                </Label>

            </CardBody>
        </Card>
    )
}