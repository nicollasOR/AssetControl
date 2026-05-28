import { api } from "./api";

type patrimonioListar ={
    patrimonioID: string,
    denominacao: string,
    numeroPatrimonio: string,
    valor: string,
    imagem: string,
    localizacaoID: string
    statusPatrimonioID: string
}

export class patrimonio_TSX {
    static toFormData(dados: patrimonioListar): FormData{
        const formData = new FormData

        formData.append("patrimonioID", dados.patrimonioID)
        formData.append("denominacao", dados.denominacao)
        formData.append("numeroPatrimonio", dados.numeroPatrimonio)
        formData.append("valor", dados.valor)
        if (dados.imagem) {
            formData.append("imagem", dados.imagem);
        }
        formData.append("localizacaoID", dados.localizacaoID)
        formData.append("statusPatrimonioID", dados.statusPatrimonioID)

        return formData
    }

    static toImagemURL(patrimonio: patrimonioListar){
        return{
                        ...patrimonio,
            imagemURL: `${api.defaults.baseURL}${patrimonio.imagem}`
        }
        
    }
}
export async function listarPatrimonio ()
{
    try{
        const response = await api.get("Patrimonio")
        
        const patrimonios = response.data.filter((patrimonioVar: patrimonioListar) => {
            patrimonioVar.statusPatrimonioID != null
        })

        return patrimonios
    }

    catch(error: any){
        throw new Error(error.response.data)
    }

}

export async function adicionarPatrimonio(){

}