import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Row } from "reactstrap"
import { getAllIngredients } from "../../modules/ingredientManager"
import { addUserIngredient, getAllUserIngredients } from "../../modules/userIngredientManager"

import { Ingredient } from "./Ingredient"

export const IngredientList = () => {
    const [ingredients, setIngredients] = useState([])

    //TODO create state to hold selected ingred
    const [userIngredients, setUserIngredients] = useState([]);
    const navigate = useNavigate()

    const getIngredients = () => {
        getAllIngredients().then(p => setIngredients(p))
    }

    const getUserIngredients = () => {
        getAllUserIngredients().then(up => setUserIngredients(up))
    }

    //TODO get current useringredients
    useEffect(() => {
        getUserIngredients();
    }, []);

    useEffect(() => {
        getIngredients();
    }, []);

    const handleAddIngredients = (evt) => {
        evt.preventDefault()
        const promises = []
        //loop through created state that holds selected ingred
        userIngredients.forEach(ui => {
            promises.push(addUserIngredient({ ingredientId: ui.ingredientId }))
        })

        Promise.all(promises).then(() => navigate('/userIngredients'))
    }


    return (
        <>
            <h3>Ingredient Library</h3>
            <div className="ingredientContainer" >
                {

                    <Button onClick={() => navigate('/ingredients/create')}>Create an Ingredient</Button>

                }


                <Row>
                    {ingredients.map((ingredient) => (

                        <Ingredient ingredient={ingredient} key={ingredient.id} userIngredients={userIngredients} setUserIngredients={setUserIngredients} />
                    ))}
                </Row>

                <Button onClick={handleAddIngredients}>Add</Button>
            </div>
        </>
    )
}