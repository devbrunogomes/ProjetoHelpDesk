import styles from "./styles.module.scss";

import { Navbar } from "../Navbar/Navbar";
import { CriarChamado } from "../CriarChamado/CriarChamado";

export const DashboardCliente = (props) => {
  return (
    <>
      <Navbar />
      <CriarChamado/>
    </>
  );
};
