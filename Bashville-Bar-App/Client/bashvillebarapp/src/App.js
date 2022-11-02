import "./App.css";
import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router } from "react-router-dom";
import ApplicationViews from "./components/ApplicationViews";
import { onLoginStatusChange } from "./modules/authManager";
import Header from "./components/Header";
import { Spinner } from "reactstrap";

function App() {

  const [isLoggedIn, setIsLoggedIn] = useState(null);

  useEffect(() => {
    onLoginStatusChange(setIsLoggedIn);
  }, []);

  //TODO Authentication here?
  if (isLoggedIn === null) {
    return <Spinner className="app-spinner dark" />;
  }

  return (
    //TODO Look into background options on reactstrap
    <Router>
      <Header isLoggedIn={isLoggedIn} />
      <ApplicationViews isLoggedIn={isLoggedIn} />
    </Router>
  );
}

export default App;
