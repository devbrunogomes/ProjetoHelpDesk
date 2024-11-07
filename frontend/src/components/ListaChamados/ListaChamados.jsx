import styles from "./styles.module.scss";
import { useEffect, useState } from "react";
import axios from "axios";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";

export const ListaChamados = (props) => {
  const [isContentVisible, setIsContentVisible] = useState(false);

  const toggleContentVisibility = () => {
    setIsContentVisible(!isContentVisible); // Alterna visibilidade
  };

  return (
    <div className={styles.container}>
      <div className={styles.title} onClick={toggleContentVisibility}>
        <h1>Listar Chamados </h1>
        {isContentVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isContentVisible && (
        <div>
          {/* {clientes.map((cliente) => (
            <div key={cliente.clienteId} className={styles.content}>
              <span>#0{cliente.clienteId}</span>
              <span>
                {cliente.nomeCompleto} - {cliente.userName}
              </span>
              <span>{cliente.email}</span>
              <div>
                <button
                  onClick={async () => await handleExcluir(cliente.clienteId)}
                >
                  Excluir
                </button>
              </div>
            </div>
          ))} */}
        </div>
      )}
    </div>
  );
}