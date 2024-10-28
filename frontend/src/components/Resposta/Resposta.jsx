import styles from "./styles.module.scss";

export const Resposta = ({ resposta }) => {
  return (
    <div className={styles.container}>
      <div>
        <h3>Respostas</h3>
      </div>
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
      <div>
        <h3>Reponder</h3>
      </div>
      
    </div>
  );
};
