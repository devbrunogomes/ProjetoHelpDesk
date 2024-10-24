import styles from "./styles.module.scss";
import { Cadastro } from "../Cadastro/Cadastro";
import { Login } from "../Login/Login";

export const TelaInicial = () => {
  return (
    <main className={styles.container}>
      <Cadastro />
      <hr className={styles.hr_line} />
      <Login />
    </main>
  );
};
