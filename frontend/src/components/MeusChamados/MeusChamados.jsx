import { Chamado } from "../Chamado/Chamado";
import { Resposta } from "../Resposta/Resposta";
import styles from "./styles.module.scss";

export const MeusChamados = (props) => {
  return (
    <div className={styles.container}>
      <div>
        <h1>MEUS CHAMADOS</h1>
      </div>
      <Chamado/>
    </div>
  );
};
