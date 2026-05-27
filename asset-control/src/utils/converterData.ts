export function converterData(data: string)
{
    
    const dataNova = new Date(data).toLocaleDateString("pt-BR")
    return dataNova
}