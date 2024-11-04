import { useEffect } from "react";
import styles from "./styles.module.scss";

export const Tecnico = ({ tecnico }) => {  
  return (
    <section className={styles.container}>
      <div className={styles.title}>
        <h1>#0{tecnico.tecnicoId} - {tecnico.userName}</h1>
      </div>
      <div className={styles.content}>
        <p>
          <strong>Nome Completo: </strong> {tecnico.nomeCompleto}
        </p>
        <p>
          <strong>CEP:</strong> {tecnico.cep}
        </p>
        <p>
          <strong>Email:</strong> {tecnico.email}
        </p>
      </div>

      <div className={styles.chamados}>
        <div className={styles.subtitulo}>
          <h1>Chamados</h1>
        </div>
      </div>
    </section>
  );
};
