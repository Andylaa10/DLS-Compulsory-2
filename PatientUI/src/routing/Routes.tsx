
import {Login} from "../pages/login/Login.tsx";
import {NotFound} from "../pages/notFound/NotFound.tsx";
import Home from "../pages/home/Home.tsx";
import {Route, BrowserRouter as Router, Routes } from "react-router-dom";


const RoutesComponent: React.FunctionComponent = () => {
  return (
    <Routes>
      <Route path="*" element={<NotFound />} />
      <Route path="/" element={<Login />} />
      <Route path="login" element={<Login />} />
      <Route path="home/:ssn" element={<Home />} />
    </Routes>
  );
};

export const AppRouter: React.FunctionComponent = () => {
  return (
    <Router>
      <RoutesComponent />
    </Router>
  );
};
