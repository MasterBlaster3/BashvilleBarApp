import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button } from "reactstrap"
import { deleteIngredient, getIngredientById } from "../../modules/ingredientManager"

export const IngredientDelete = () => {
    const [ingredient, setIngredient] = useState([])
    const navigate = useNavigate()
    const { ingredientId } = useParams()

    const getIngredient = () => {
        getIngredientById(id)
            .then(res => setIngredient(res))
    };

    const handleClickDelete = () => {
        deleteIngredient(Ingredient.id)
            .then(navigate("/ingredients"))
    };

    useEffect(() => {
        getIngredient()
    }, []);

    return <>
        <h2>Would you like to remove {ingredient.name} from the ingredient library?</h2>
        <Button color="danger" onClick={() => { deleteButton() }}>Delete</Button>
        <Button onClick={() => { navigate("/ingredient") }}>Changed my mind</Button>
    </>

}