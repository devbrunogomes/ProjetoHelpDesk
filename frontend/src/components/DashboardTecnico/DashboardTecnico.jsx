import { MeusChamados } from "../MeusChamados/MeusChamados";
import { Navbar } from "../Navbar/Navbar";
import styles from "./styles.module.scss";

export const DashboardTecnico = (props) => {
  return (
    <div className={styles.container}>
      <Navbar />
      <MeusChamados/>
    </div>
  );
};
