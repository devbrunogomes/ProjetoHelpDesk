import "./App.css";
import { DashboardCliente } from "./components/DashboardCliente/DashboardCliente";
import { DashboardTecnico } from "./components/DashboardTecnico/DashboardTecnico";
import { TelaInicial } from "./components/TelaInicial/TelaInicial";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

function App() {
  return (
    <div className="container">
      <Router>
        <Routes>
          <Route path="/" element={<TelaInicial />} />
          <Route path="/cliente-dashboard" element={<DashboardCliente />} />
          <Route path="/tecnico-dashboard" element={<DashboardTecnico/>} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
