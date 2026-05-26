import styles from "./header.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircleInfo } from "@fortawesome/free-solid-svg-icons";
const Header = () => {
  return(
<>

    <header className={styles.topbar}>
        <nav className={`${styles.navbar} layout_guide`} aria-label="Menu principal">
            <a href="#" className={styles.logoLink} aria-label="Página inicial">
                <img src="../imgs/Logo Senai.png" className={styles.logo} alt="" />
            </a>

            <ul className={styles.e}>
                <li>
                    <a href="#" className={styles.menuLink}>
                        Ambientes
                        <i className="fa-solid fa-chevron-down"></i>
                    </a>
                </li>

                <li>
                    <a href="#" className={styles.menuLink}>Patrimônios</a>
                </li>
            </ul>

            <section className={styles.userArea} aria-label="Informações do usuário">
                <button className="user-icon" aria-label="Abrir perfil do usuário">
                    <i className="fa-solid fa-user"></i>
                </button>

                <div className="user-info">
                    <strong>Késsia Milena</strong>
                    <span>kessia@sp.senai.br</span>
                </div>

                <button className="arrow-button" aria-label="Abrir opções da conta">
                    <i className="fa-solid fa-chevron-down"></i>
                </button>
            </section>
            <button className="hamburguer" aria-label="Abrir opções de menu ">
                <i className="fa-solid fa-bars"></i>
            </button>
        </nav>
    </header>

</>


)
};

export default Header;
