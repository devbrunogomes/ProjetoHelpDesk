import styles from "./styles.module.scss";
import { Navbar } from "../Navbar/Navbar";
import { CadastroTecnicoAdm } from "../CadastroTecnicoAdm/CadastroTecnicoAdm";
import { ListaTecnicos } from "../ListaTecnicos/ListaTecnicos";
import { PesquisarTecnicos } from "../PesquisarTecnico/PesquisarTecnico";
import { ListaClientes } from "../ListaClientes/ListaClientes";
import { PesquisarCliente } from "../PesquisarCliente/PesquisarCliente";

export const DashboardAdm = (props) => {
  return (
    <div className={styles.container}>
      <Navbar />
      <CadastroTecnicoAdm/>
      <ListaTecnicos/>
      <PesquisarTecnicos/>
      <ListaClientes/>
      <PesquisarCliente/>
    </div>
  );
};
