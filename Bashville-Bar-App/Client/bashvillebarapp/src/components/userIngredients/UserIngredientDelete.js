import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button } from "reactstrap"
import { getAllUserIngredients, getUserIngredientById } from "../../modules/userIngredientManager"


export const DeleteOldIngredientsList = () => {
    const [userIngredients, setUserIngredients] = useState([])
    // const [allIngredients, setAllUserIngredients] = useState([])
    const navigate = useNavigate();

    useEffect(() => {
        getAllUserIngredients().then(userIng => setUserIngredients(userIng))
    }, [])

    // useEffect(() => {
    //     getAllUserIngredients().then(allIng => setAllUserIngredients(allIng))
    // }, [])

    const SaveButtonClick = () => {
        return updateUsersIngredientsInApi(ingredientsArray)
            .then(() => {
                navigate(-1)
            })
    }

    return (
        <div className="container">
            <div>
                <Button color="success" onClick={SaveButtonClick}><h6>Save Updated Ingredients</h6></Button>
                <Button color="danger" onClick={() => navigate(-1)}><h6>Cancel</h6></Button>
            </div>
            <div className="row justify-content-center">
                {allIngredients.map((ingredient) => (
                    <IngredientCheckBox
                        ingredient={ingredient}
                        key={ingredient.id}
                        ingredientsArray={ingredientsArray}
                        setIngredientsArray={setIngredientsArray}
                    />
                ))}
            </div>
        </div>
    )

}