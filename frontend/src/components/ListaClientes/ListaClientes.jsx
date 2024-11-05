import styles from "./styles.module.scss";
import { useEffect, useState } from "react";
import axios from "axios";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";


export const ListaClientes = (props) => {
  const [isContentVisible, setIsContentVisible] = useState(false);
  const [clientes, setClientes] = useState([]);

  const toggleContentVisibility = () => {
    setIsContentVisible(!isContentVisible); // Alterna visibilidade
  };

  async function getClientes() {
    const token = localStorage.getItem("token");

    try {
      const response = await axios.get("http://localhost:5089/Cliente", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      setClientes(response.data);
      console.log(clientes);
    } catch (error) {
      console.error("Erro ao carregar os técnicos:", error);
    }
  }

  const handleExcluir = async (id) => {
    const token = localStorage.getItem("token");

    try {
      // Realiza a requisição DELETE para o endpoint
      await axios.delete(`http://localhost:5089/Cliente/${id}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      // Atualiza o estado `tecnicos`, removendo o técnico excluído
      setClientes((prevClientes) =>
        prevClientes.filter((cliente) => cliente.clienteId !== id)
      );
      console.log("Cliente excluído com sucesso!");
    } catch (error) {
      console.error("Erro ao excluir o técnico:", error);
    }
  };


  useEffect(() => {
    getClientes();
  }, []);

  return (
    <div className={styles.container}>
      <div className={styles.title} onClick={toggleContentVisibility}>
        <h1>Listar Clientes </h1>
        {isContentVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isContentVisible && (
        <div>
          {clientes.map((cliente) => (
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
          ))}
        </div>
      )}
    </div>
  );
};
