import styles from "./styles.module.scss";
import { Navbar } from "../Navbar/Navbar";
import { CadastroTecnicoAdm } from "../CadastroTecnicoAdm/CadastroTecnicoAdm";

export const DashboardAdm = (props) => {
  return (
    <div className={styles.container}>
      <Navbar />
      <CadastroTecnicoAdm/>
    </div>
  );
};
