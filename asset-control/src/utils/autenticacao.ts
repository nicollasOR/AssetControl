import secureLocalStorage from "react-secure-storage";

export function verificarAutenticacao(){
    const token = secureLocalStorage.getItem("Token")

    return !!token; // o !!variavel = transforma automaticamente a variavel em booleano e verifica se existe (se existir === true), se nao (=== false) 
}

