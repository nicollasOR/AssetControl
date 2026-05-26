import secureLocalStorage from "react-secure-storage";
import { api } from "./api";

export async function login(NIF: string, senha: string){
try{
    const response = await api.post("Autenticacao/login", {NIF, senha});
    const token = response.data.token;
    secureLocalStorage.setItem("Token", token)
    
}

catch(erro: any)
{
    throw new Error("NIF inválido")
}
}