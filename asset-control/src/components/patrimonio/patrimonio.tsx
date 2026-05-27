import { faArrowRightArrowLeft, faCircleInfo } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Link from "next/link";
import { converterData } from "@/src/utils/converterData";
import styles from './paginacao.module.css'
type Patrimonio = {
    patrimonioId: string
    patrimonio: string,
    denominacao: string,
    tipo: string,
    dataTransferencia: string,
    coordenadorAutenticado: boolean
    onTransferir: boolean

}

const cardPatrimonio = ({
    patrimonioId,
    patrimonio,
    denominacao,
    tipo,
    dataTransferencia,
    coordenadorAutenticado,
    onTransferir

}: Patrimonio) => {
  return (
    <>
      <tr className={styles.tabela} >
        <td>{patrimonio}</td>
        <td>{denominacao}</td>
        <td>{tipo}</td>
        <td>{converterData(dataTransferencia)}</td>

        <td>
          <Link href={"patrimonio?id=" + patrimonioId}><a href="#" aria-label="Ver detalhes do patrimonio">
            <FontAwesomeIcon icon={faCircleInfo} />
          </a>
          </Link>
        </td>

        <td>
          <a href="#" aria-label="Transferir patrimonio">
            <Link href={""}><FontAwesomeIcon icon={faArrowRightArrowLeft} /></Link>
          </a>
        </td>
      </tr>
    </>
  );
};

export default cardPatrimonio;
