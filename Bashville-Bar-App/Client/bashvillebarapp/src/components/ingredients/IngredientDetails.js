import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Card, CardBody, Button } from "reactstrap";
import { getDetailsByName } from "../../modules/ingredientDetailsManager";


export const IngredientDetails = () => {
    const navigate = useNavigate();
    const { id } = useParams();

    const [details, setDetails] = useState({
        strIngredient: '',
        strDescription: ''
    })

    const getDetails = () => {
        getDetailsByName(id)
            .then(res => setDetails(res.ingredients[0]))

    }
    useEffect(() => {
        getDetails()
    }, []);


    return (
        <div>
            <Card id="card" >
                <CardBody>
                    <h2>{`${details.strIngredient}`}</h2>
                    {details.strDescription != null ? <h6>{`${details.strDescription}`}</h6> : <h6>No details in the database</h6>}
                    <Button onClick={() => navigate(`/ingredients`)}>Return</Button>
                    <Button onClick={(() => navigate(`/ingredients/delete/${id}`))}>Delete</Button>
                </CardBody>
            </Card>
        </div>
    )
}