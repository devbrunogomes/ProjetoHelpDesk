import styles from "./styles.module.scss";

export const Resposta = ({ resposta }) => {
  return (
    <div className={styles.container}>
      
      <div className={styles.respostas}>
        <div>
          <span>{resposta.data} -</span>
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
