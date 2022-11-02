import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { Card, CardBody, FormGroup, Input, Label } from "reactstrap";
import { NavLink as RRNavLink } from "react-router-dom";

//TODO Get selected ingreds as a prop
export const Ingredient = ({ ingredient, userIngredients, setUserIngredients }) => {
    const navigate = useNavigate()


    const isStocked = () => {
        if (!userIngredients) {
            return false
        }
        for (const ui of userIngredients) {
            if (ui.ingredientId === ingredient.id) {
                return true
            }
        }
        return false
    }

    const handleChange = (event) => {

        const userIngredientCopy = structuredClone(userIngredients)
        if (event.target.checked) {
            userIngredientCopy.push({ ingredientId: ingredient.id })
            setUserIngredients(userIngredientCopy)
        }
        else {
            setUserIngredients(userIngredientCopy.filter((booze) => booze.ingredientId !== ingredient.id))
        }
    }


    return (
        <Card id="card">
            <CardBody>
                <FormGroup check>
                    <Label check>
                        <Input
                            onChange={handleChange}
                            className="form-check-input"
                            //TODO logic to precheck checkbox if ingredient is already in place
                            type="checkbox"
                            defaultChecked={isStocked()}
                            name="selectedIngredients"
                            id={ingredient.id}
                        />
                        <Link to={`/ingredients/details/${ingredient.name}`}>
                            <h6>{`${ingredient.name}`}</h6>
                        </Link>
                    </Label>
                </FormGroup>
            </CardBody>
        </Card>
    )
}