using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Enum
{
    public enum RamoAtividade
    {

        [Display(Name = "Não consta atividade AVSEC.")]
        RamoAtividade0,
        [Display(Name = "Atendimento do Passageiro no checkin (despacho de passageiro) ou identificação e aceitação(conciliação) de bagagem despachada.")]
        RamoAtividade1,
        [Display(Name = "Supervisionar o trânsito depassageiros entre a área de embarque e a aeronave")]
        RamoAtividade2,
        [Display(Name = "Atendimento a Passageiro em voo")]
        RamoAtividade3,
        [Display(Name = "Operar aeronave em exploração de serviço de transporte aéreo público")]
        RamoAtividade4,
        [Display(Name = "Avaliar os projetos de obras aeroportuárias, para garantir que os aspectos da AVSEC estejam contemplados na concepção e execução dos projetos ")]
        RamoAtividade5,
        [Display(Name = "Identificação e controle de acesso de pessoas e objetos às áreas restritas ou controladas de aeroportos.")]
        RamoAtividade6,
        [Display(Name = "Identificação e controle de acesso de pessoas à aeronave")]
        RamoAtividade7,
        [Display(Name = "Identificação e inspeção de pessoas e veículos em controle de acesso de veículos")]
        RamoAtividade8,
        [Display(Name = "Inspeção das Provisões de Bordo ou serviço de bordo")]
        RamoAtividade9,
        [Display(Name = "Inspeção de Área Estéril")]
        RamoAtividade10,
        [Display(Name = "Inspeção de bagagens, carga e objetos em geral por meio de raios X ou tomógrafos")]
        RamoAtividade11,
        [Display(Name = "Inspeção de bagagens, carga e objetos em geral por meio de cão farejador de explosivos")]
        RamoAtividade12,
        [Display(Name = "Inspeção de pessoas por meio de body - scan")]
        RamoAtividade13,
        [Display(Name = "Inspeção de pessoas")]
        RamoAtividade14,
        [Display(Name = "Inspeção de segurança da aeronave")]
        RamoAtividade15,
        [Display(Name = "Inspeção manual de objetos")]
        RamoAtividade16,
        [Display(Name = "Patrulhamento e vigilância das instalações de produção e armazenamento de serviço de bordo e terminais de carga ou correio")]
        RamoAtividade17,
        [Display(Name = "Patrulhamento e vigilância da área operacional de aeroportos")]
        RamoAtividade18,
        [Display(Name = "Patrulhamento e vigilância de canais de controle de acesso")]
        RamoAtividade19,
        [Display(Name = "Realizar vigilância em provisões de bordo, serviço de bordo, carga ou correio ou bagagens despachadas")]
        RamoAtividade20,
        [Display(Name = "Consolidação do Despacho AVSEC do voo")]
        RamoAtividade21,
        [Display(Name = "Proteção de área estéril ou de aeronave")]
        RamoAtividade22,
        [Display(Name = "Recebimento das Provisões na Aeronave ")]
        RamoAtividade23,
        [Display(Name = "Recebimento e identificação de carga ou correio na cadeia segura da carga")]
        RamoAtividade24,
        [Display(Name = "Revista (busca pessoal) em pessoas")]
        RamoAtividade25,
        [Display(Name = "Verificação de Segurança da Aeronave")]
        RamoAtividade26,
        [Display(Name = "Coordenar e gerir setor de segurança aeroportuária ")]
        RamoAtividade27,
        [Display(Name = "Supervisionar e garantir a implementação dos controles de segurança e medidas de resposta pelo operador aéreo em âmbito nacional ou de aeródromo.")]
        RamoAtividade28,
        [Display(Name = "Supervisionar e garantir a implementação dos controles de segurança previstos no Programa de Segurança de Agentes de carga aérea acreditados(PSACA)")]
        RamoAtividade29,
        [Display(Name = "Supervisionar e monitorar a inspeção e revista de passageiros e bagagens")]
        RamoAtividade30,
        [Display(Name = "Supervisionar e avaliar o treinamento em serviço da certificação em inspeção de segurança")]
        RamoAtividade31,
        [Display(Name = "Representar operador aéreo ou de aeródromo em eventos de segurança exigidos em norma, como CSA, ESAIA e ESAB")]
        RamoAtividade32,
        [Display(Name = "Realizar atividade de controle de qualidade AVSEC, para operador aeroportuário, exceto testes")]
        RamoAtividade33,
        [Display(Name = "Realizar atividade de controle de qualidade AVSEC, para operador aéreo, exceto testes")]
        RamoAtividade34,
        [Display(Name = "Realizar testes de controle de qualidade")]
        RamoAtividade35,
        [Display(Name = "Ministrar curso AVSEC e controlar frequência dos alunos.")]
        RamoAtividade36,
        [Display(Name = "Produzir materiais instrucionais paracursos AVSEC")]
        RamoAtividade37
    }
}
