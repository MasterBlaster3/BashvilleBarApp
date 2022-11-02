import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addIngredient } from "../../modules/ingredientManager"
import { IngredientList } from "./IngredientList"

export const IngredientCreate = () => {
    const navigate = useNavigate()
    const [ingredient, updateIngredient] = useState({
        name: ""
    })

    const handleCreateButtonClick = (event) => {
        event.preventDefault()
        const ingredientToSendToApi = {
            Name: ingredient.name,
        }
        if (ingredientToSendToApi.Name != null) {
            addIngredient(ingredientToSendToApi)
            return navigate("/ingredients")
        } else { window.alert("Please fill out all form inputs.") }
    }

    return (
        <>
            <Form className="createForm">
                <FormGroup>
                    <fieldset>
                        <div className="form-description">
                            <label htmlFor="name">Ingredient Name:</label>
                            <input type="name"

                                placeholder="Enter ingredient name..."
                                onChange={
                                    (evt) => {
                                        let copy = { ...ingredient }
                                        copy.name = evt.target.value
                                        updateIngredient(copy)
                                    }
                                } />

                            <div className="createButtons">
                                <button onClick={(clickEvent) => { handleCreateButtonClick(clickEvent) }}
                                >Add new ingredient</button>
                                <button onClick={() => { navigate("/ingredients") }}
                                >Cancel</button>
                            </div>
                        </div>
                    </fieldset>

                </FormGroup>
            </Form>
        </>
    )
}