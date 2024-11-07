import { useEffect, useState } from "react";
import styles from "./styles.module.scss";
import { Chamado } from "../Chamado/Chamado";

export const Cliente = ({ cliente }) => {
  const [chamados, setChamados] = useState(cliente.chamados || []);

  return (
    <section className={styles.container}>
      <div className={styles.title}>
        <h1>
          #0{cliente.clienteId} - {cliente.userName}
        </h1>
      </div>
      <div className={styles.content}>
        <p>
          <strong>Nome Completo: </strong> {cliente.nomeCompleto}
        </p>
        <p>
          <strong>CEP:</strong> {cliente.cep}
        </p>
        <p>
          <strong>Email:</strong> {cliente.email}
        </p>
      </div>

      <div className={styles.subtitulo}>
        <h1>Chamados</h1>
      </div>
      <div className={styles.chamados}>
        {chamados.map((chamado) => (
          <Chamado key={chamado.chamadoId} chamado={chamado} />
        ))}
      </div>
    </section>
  );
};
