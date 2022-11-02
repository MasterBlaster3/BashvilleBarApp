import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import { IngredientList } from "./ingredients/IngredientList";
import Hello from "./Hello";
import { IngredientCreate } from "./ingredients/IngredientCreate";
import { IngredientDetails } from "./ingredients/IngredientDetails";
import { UserIngredientList } from "./userIngredients/UserIngredientList";


export default function ApplicationViews({ isLoggedIn }) {
    return (
        <main>
            <Routes>
                <Route path="/">
                    <Route index element={isLoggedIn ? <Hello /> : <Navigate to="/login" />}
                    />
                    <Route path="register" element={<Register />} />


                    <Route path="ingredients/" >
                        <Route index element={isLoggedIn ? <IngredientList /> : <Navigate to="/login" />} />
                        <Route path="create" element={isLoggedIn ? <IngredientCreate /> : <Navigate to="/login" />} />
                        <Route path="details/:id" index element={isLoggedIn ? <IngredientDetails /> : <Navigate to="/login" />} />
                        <Route path="delete/:id" index element={isLoggedIn ? <IngredientDetails /> : <Navigate to="/login" />} />
                    </Route>

                    <Route path="userIngredients">
                        <Route index element={isLoggedIn ? <UserIngredientList /> : <Navigate to="/login" />} />


                    </Route>


                    <Route path="*" element={<p>Whoops, nothing here...</p>} />
                </Route>
            </Routes>
        </main>
    );
};