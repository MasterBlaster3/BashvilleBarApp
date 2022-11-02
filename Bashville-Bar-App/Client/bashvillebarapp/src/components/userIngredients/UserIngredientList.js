import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Table } from "reactstrap"
import { getAllUserIngredients } from "../../modules/userIngredientManager"
import { UserIngredient } from "./UserIngredient"

export const UserIngredientList = () => {
    const [userIngredients, setUserIngredients] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        getAllUserIngredients()
            .then(res => setUserIngredients(res))
    }, []);


    return (
        <div className="userBar">
            <Table>
                {userIngredients.map((userIngredient) => (
                    <UserIngredient userIngredient={userIngredient} key={userIngredient.id} />
                ))}
            </Table>

        </div>
    )
}