import styles from "./styles.module.scss";

import { Navbar } from "../Navbar/Navbar";
import { CriarChamado } from "../CriarChamado/CriarChamado";
import { MeusChamados } from "../MeusChamados/MeusChamados";
import { TrocarSenha } from "../TrocarSenha/TrocarSenha";

export const DashboardCliente = (props) => {
  return (
    <div className={styles.container}>
      <Navbar />
      <CriarChamado />
      <MeusChamados/>
      <TrocarSenha/>
    </div>
  );
};
