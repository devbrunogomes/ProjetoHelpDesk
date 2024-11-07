import styles from "./styles.module.scss";
import * as handlerEnum from "../../functions/HandleEnumFromJson";

export const Resposta = ({ resposta }) => {
  return (
    <div className={styles.container}>
      
      <div className={styles.respostas}>
        <div>
          <span>{handlerEnum.formatarDataHora(resposta.data)} -</span>
          <span> {resposta.autor}</span>
        </div>
        <div>
          <p>
            {resposta.mensagem}
          </p>
        </div>
      </div>      
      
    </div>
  );
};
