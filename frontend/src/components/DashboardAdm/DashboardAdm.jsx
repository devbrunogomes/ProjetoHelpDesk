import styles from "./styles.module.scss";
import { Navbar } from "../Navbar/Navbar";
import { CadastroTecnicoAdm } from "../CadastroTecnicoAdm/CadastroTecnicoAdm";
import { ListaTecnicos } from "../ListaTecnicos/ListaTecnicos";
import { PesquisarTecnicos } from "../PesquisarTecnico/PesquisarTecnico";
import { ListaClientes } from "../ListaClientes/ListaClientes";
import { PesquisarCliente } from "../PesquisarCliente/PesquisarCliente";
import { PesquisarChamado } from "../PesquisarChamado/PesquisarChamado";
import { TrocarSenha } from "../TrocarSenha/TrocarSenha";
import { ChamadosAbertos } from "../ChamadosAbertos/ChamadosAbertos";
import { GraficoChamados } from "../GraficoChamados/GraficoChamados";
import { GraficoTecnicos } from "../GraficoTecnicos/GraficoTecnicos";
import { ListaChamados } from "../ListaChamados/ListaChamados";

export const DashboardAdm = (props) => {
  return (
    <div className={styles.container}>
      <Navbar />
      <CadastroTecnicoAdm />
      <ListaTecnicos />
      <PesquisarTecnicos />
      <ListaClientes />
      <PesquisarCliente />
      <ListaChamados/>
      <PesquisarChamado />
      <GraficoChamados />
      <GraficoTecnicos />
      <TrocarSenha />
    </div>
  );
};
