import styles from "./header.module.css";

const Header = () => {
  return(
  <header className="topbar">
    <nav className="navbar layout_guide" aria-label="Menu principal">
      <a href="#" className="logo-link" aria-label="Página inicial">
        <img src="../imgs/Logo Senai.png" alt="Logo SENAI" className="logo" />
      </a>
      <ul className="menu-list">
        <li>
          <a href="#" className="menu-link">
            patrimonios
            <i className="fa-solid fa-chevron-down" />
          </a>
        </li>
        <li>
          <a href="#" className="menu-link">
            Patrimônios
          </a>
        </li>
      </ul>
      <section className="user-area" aria-label="Informações do usuário">
        <button className="user-icon" aria-label="Abrir perfil do usuário">
          <i className="fa-solid fa-user" />
        </button>
        <div className="user-info">
          <strong>Késsia Milena</strong>
          <span>kessia@sp.senai.br</span>
        </div>
        <button className="arrow-button" aria-label="Abrir opções da conta">
          <i className="fa-solid fa-chevron-down" />
        </button>
      </section>
      <button className="hamburguer" aria-label="Abrir opções de menu ">
        <i className="fa-solid fa-bars" />
      </button>
    </nav>
  </header>)
};

export default Header;
