using System.Collections.Generic;
using System.Linq;

namespace MagicEye3.Services.BackEndAPI.Utilities
{
    // ------------------------------------------------------------
    // 1) Enum: componentes de aprendizaje
    // ------------------------------------------------------------
    public enum ComponenteAprendizaje
    {
        ACD, // Aprendizaje con el Docente
        APE, // Aprendizaje Practico Experimental
        AA   // el Aprendizaje Autonomo
    }

    // ------------------------------------------------------------
    // 2) Clase BurnActividad
    // ------------------------------------------------------------
    public class BurnActividad
    {
        public int Id { get; }
        public string Descripcion { get; }
        public ComponenteAprendizaje Componente { get; }
        public string Evaluacion { get; }

        public BurnActividad(int id,
                             string descripcion,
                             ComponenteAprendizaje componente,
                             string evaluacion)
        {
            Id = id;
            Descripcion = descripcion;
            Componente = componente;
            Evaluacion = evaluacion;
        }
    }

    // ------------------------------------------------------------
    // 3) Catalogo estatico
    // ------------------------------------------------------------
    public static class CatalogoActividades
    {
        public static readonly IReadOnlyList<BurnActividad> Todas =
            new List<BurnActividad>
            {
                // ---------- ACD ----------
                new BurnActividad( 1, "Clase Magistral",                 ComponenteAprendizaje.ACD, "Cuestionario en Moodle"),
                new BurnActividad( 2, "Discusion guiada",                ComponenteAprendizaje.ACD, "Rubrica de participacion"),
                new BurnActividad( 3, "Estudio de casos",                ComponenteAprendizaje.ACD, "Informe escrito y presentacion oral"),
                new BurnActividad( 4, "Debate academico",                ComponenteAprendizaje.ACD, "Rubrica de argumentacion y participacion"),
                new BurnActividad( 5, "Tutoria grupal",                  ComponenteAprendizaje.ACD, "Registro de asistencia y participacion"),
                new BurnActividad( 6, "Seminario tematico",              ComponenteAprendizaje.ACD, "Ensayo critico evaluado"),
                new BurnActividad( 7, "Análisis de lecturas",            ComponenteAprendizaje.ACD, "Prueba escrita de comprension"),
                new BurnActividad( 8, "Resolucion de problemas en clase",ComponenteAprendizaje.ACD, "Lista de cotejo de procedimientos"),
                new BurnActividad( 9, "Exposicion oral",                 ComponenteAprendizaje.ACD, "Rubrica de exposicion"),
                new BurnActividad(10, "Revision de tareas",              ComponenteAprendizaje.ACD, "Retroalimentacion escrita"),

                // ---------- APE ----------
                new BurnActividad(11, "Practica de laboratorio",         ComponenteAprendizaje.APE, "Revision y discusion de resultados"),
                new BurnActividad(12, "Simulacion de procesos",          ComponenteAprendizaje.APE, "Observacion directa y lista de cotejo"),
                new BurnActividad(13, "Taller practico",                 ComponenteAprendizaje.APE, "Producto final y rubrica de desempeno"),
                new BurnActividad(14, "Proyecto tecnico",                ComponenteAprendizaje.APE, "Informe tecnico y presentacion"),
                new BurnActividad(15, "Practica en campo",               ComponenteAprendizaje.APE, "Diario de campo y evaluacion de desempeno"),
                new BurnActividad(16, "Ensayo de procedimientos",        ComponenteAprendizaje.APE, "Registro de observacion y retroalimentacion"),
                new BurnActividad(17, "Demostracion de habilidades",     ComponenteAprendizaje.APE, "Lista de cotejo de habilidades"),
                new BurnActividad(18, "Resolucion de casos practicos",   ComponenteAprendizaje.APE, "Informe de solucion y discusion"),
                new BurnActividad(19, "Uso de software especializado",   ComponenteAprendizaje.APE, "Tareas practicas evaluadas"),
                new BurnActividad(20, "Diseno de prototipos",            ComponenteAprendizaje.APE, "Evaluacion del prototipo y presentacion"),

                // ---------- AA ----------
                new BurnActividad(21, "Investigacion documental",        ComponenteAprendizaje.AA,  "Revision y discusion"),
                new BurnActividad(22, "Lectura critica",                 ComponenteAprendizaje.AA,  "Ensayo reflexivo"),
                new BurnActividad(23, "Elaboracion de mapas conceptuales",ComponenteAprendizaje.AA, "Evaluacion del mapa y autoevaluacion"),
                new BurnActividad(24, "Resolucion de ejercicios",        ComponenteAprendizaje.AA,  "Entrega de ejercicios resueltos"),
                new BurnActividad(25, "Desarrollo de proyectos",         ComponenteAprendizaje.AA,  "Informe de avance y producto final"),
                new BurnActividad(26, "Estudio de videos educativos",    ComponenteAprendizaje.AA,  "Cuestionario de comprension"),
                new BurnActividad(27, "Participacion en foros virtuales",ComponenteAprendizaje.AA,  "Rubrica de participacion y aportes"),
                new BurnActividad(28, "Elaboracion de portafolios",      ComponenteAprendizaje.AA,  "Revision del portafolio y reflexion escrita"),
                new BurnActividad(29, "Autoevaluacion de aprendizajes",  ComponenteAprendizaje.AA,  "Cuestionario de autoevaluacion"),
                new BurnActividad(30, "Preparacion de presentaciones",   ComponenteAprendizaje.AA,  "Evaluacion de la presentacion y contenido")
            };

        // --- Helpers de consulta ---------------------------------
        public static IEnumerable<BurnActividad> PorComponente(ComponenteAprendizaje comp) =>
            Todas.Where(a => a.Componente == comp);

        public static BurnActividad? PorId(int id) =>
            Todas.FirstOrDefault(a => a.Id == id);
    }
}
