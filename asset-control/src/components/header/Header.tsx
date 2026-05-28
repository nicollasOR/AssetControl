import styles from "./header.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircleInfo, faChevronDown, faUser, faBars } from "@fortawesome/free-solid-svg-icons";
import Link from "next/link";
import EX_Imp_Card from "../impor_export/exportarComponent";
const Header = () => {
  return(
<>

   <header className={styles.topbar}>
    <nav
        className={`${styles.navbar} layout_guide`}
        aria-label="Menu principal"
    >
        <a
            href="#"
            className={styles.logo_link}
            aria-label="Página inicial"
        >
            {/* <Link href={""}> */}
            <img
                src="../imgs/Logo Senai.png"
                alt="Logo SENAI"
                className={styles.logo}
            />
            {/* </Link> */}
        </a>

        <ul className={styles.menu_list}>
            <li>
                <a
                    href="#"
                    className={styles.menu_link}
                >
                    Ambientes
                    <FontAwesomeIcon icon={faChevronDown} />
                </a>
            </li>

            <li>
                <a
                    href="#"
                    className={styles.menu_link}
                >
                    Patrimônios
                </a>
            </li>
        </ul>

        <section
            className={styles.user_area}
            aria-label="Informações do usuário"
        >
            <button
                className={styles.user_icon}
                aria-label="Abrir perfil do usuário"
            >
                <FontAwesomeIcon icon={faUser}/>
            </button>

            <div className={styles.user_info}>
                <strong>Késsia Milena</strong>
                <span>kessia@sp.senai.br</span>
            </div>

            <button
                className={styles.arrow_button}
                aria-label="Abrir opções da conta"
            >
                <FontAwesomeIcon icon={faChevronDown} />
            </button>
        </section>

        <button
            className={styles.hamburguer}
            aria-label="Abrir opções de menu"
        >
            <FontAwesomeIcon icon={faBars} />
        </button>
    </nav>
</header>

</>


)
};

export default Header;
