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

const CardPatrimonio = ({
    patrimonioId,
    patrimonio,
    denominacao,
    tipo,
    dataTransferencia,
    coordenadorAutenticado,
    onTransferir

}: Patrimonio) => {

  const teste = new Date(dataTransferencia).toLocaleDateString("pt-BR")
  console.log(`oi para todos, ${teste}` )
  return (
    // <>
      <tr className={styles.tabela} key={patrimonioId}>
        <td>{patrimonio}</td>
        <td>{denominacao}</td>
        <td>{tipo}</td>
        <td>{new Date(dataTransferencia).toLocaleDateString("pt-BR")}</td>
        
        

        <td>
          <Link href={"patrimonio?id=" + patrimonioId}> 
            <FontAwesomeIcon icon={faCircleInfo} />
          </Link>
        </td>

        <td>
            <Link href={"/detalhe-patrimonio"}><FontAwesomeIcon icon={faArrowRightArrowLeft} /></Link>
        </td>
      </tr>
    // </>
  );
};

export default CardPatrimonio;
