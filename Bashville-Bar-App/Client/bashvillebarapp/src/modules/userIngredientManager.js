
import "firebase/auth";
import { getToken } from "./authManager";
const baseUrl = '/api/UserIngredient';

export const getAllUserIngredients = () => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        })
            .then((res) => res.json())
    })

}

export const getUserIngredientById = (id) => {
    return fetch(baseUrl + `/${id}`)
        .then((res) => res.json())
}

export const deleteUserIngredient = (id) => {
    return fetch(baseUrl + `/delete/${id}`, {
        method: "DELETE",
    })
}

export const addUserIngredient = (ingredient) => {
    return getToken().then((token) => {
        return fetch(baseUrl, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`
            },
            body: JSON.stringify(ingredient)
        })
    })
}

export const editUserIngredient = (id, ingredient) => {
    return fetch(baseUrl + `/${id}`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(ingredient)
    })
}