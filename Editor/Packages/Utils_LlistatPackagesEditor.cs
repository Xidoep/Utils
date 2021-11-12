using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Utils_LlistatPackagesEditor : EditorWindow
{
    const string TAB = "      ";
    const string PUNT = "      - ";

    bool animacioPerCodi, anuncis, assoliments, audio, camares;
    bool cameraShake;
    bool capturar;
    bool carregatZones;
    bool cinemachine;
    bool clons;
    bool controlTemps;
    bool creadorElements;
    bool danyable;
    bool desplaçable;
    bool dialegs;
    bool direccioBasatOrientacio;
    bool escenes;
    bool esdeveniments;
    bool estadistiques;
    bool flocking;
    bool FPS;
    bool Guardat;
    bool Houdini;
    bool icones;
    bool idiomes;
    bool iluminacio;
    bool localization;
    bool lut;
    bool maquinaEstats;
    bool Morphing;
    bool Moviment3D;
    bool Moviment25D;
    bool MySQL;
    bool Notificacions;
    bool ObjectPooling;
    bool PlataformaConexio;
    bool PlataformaRanking;
    bool PoliticaPrivacitat;
    bool Procedural;
    bool ProceduralAnim;
    bool RagDollDinamic;
    bool Renderitzat;
    bool Selector;
    bool ShaderLRP;
    bool SquashAndStretch;
    bool Temps;
    bool UI;
    bool Utils;

    [MenuItem("XidoStudio/Packages")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(Utils_LlistatPackagesEditor));

    }

    //TOT //0
    public string[] TAGS = new string[] 
    {
        "",
        TAG_FACILITADOR,
        TAG_OPTIMITZADOR,
        TAG_GENERICS,
        TAG_EXEMPLE,
        TAG_ASSETS,
        TAG_CAMARA,
        TAG_MOBIL,
        TAG_EFECTE,
        TAG_ENGAGEMENT,
        TAG_SCRIPTABLEOBJECT,
        TAG_EDITOR,
        TAG_PENDENT
    };
    const string TAG_FACILITADOR = "[Facil]"; //Ajuda a fer coses que serien mes complivades o laborioses sense aquesta eina.
    const string TAG_OPTIMITZADOR = "[Optim]"; //Millora, optimitza o fa mes rapid certes coses.
    const string TAG_GENERICS = "[Generic]"; //Coses ja preparades per utilitzar com a base, o plantilla.
    const string TAG_EXEMPLE = "[Ex]"; //Exemples de com fer o utilitzar coses, segurament no utilitzables.
    const string TAG_ASSETS = "[Asset]"; //Assets per utilitzar
    const string TAG_CAMARA = "[Cam]"; //Coses que afectes a la camara o a la visualitzacio
    const string TAG_MOBIL = "[Mobil]"; //Enfocat parcial o exlusivametn per mobil
    const string TAG_EFECTE = "[Eff]"; //Crea o gestiona efectes visuals.
    const string TAG_PENDENT = "[Pendent]"; //Està per fer.
    const string TAG_ENGAGEMENT = "[Engage]"; //Destinat a millorar l'experiencia/fidelitzacio del jugador.
    const string TAG_SCRIPTABLEOBJECT = "[Scriptable]"; //Utilitza scriptableObjects.
    const string TAG_EDITOR = "[Editor]"; //Extensions de l'editor.

    int tag;
    public List<string> tags = new List<string>();
    Vector2 scrollView;

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        for (int i = 0; i < TAGS.Length; i++)
        {
            BotoTag(TAGS[i]);
        }
        EditorGUILayout.EndHorizontal();

        scrollView = EditorGUILayout.BeginScrollView(scrollView);
        animacioPerCodi = Grup(animacioPerCodi, $"ANIMACIO PER CODI: Anims simples i barates. {TAG_OPTIMITZADOR}{TAG_SCRIPTABLEOBJECT}");
        if (animacioPerCodi)
        {
            EditorGUILayout.LabelField($"{PUNT}Affegir component: AnimacioPerCodi");
            EditorGUILayout.Space();
            EditorGUILayout.LabelField($"{TAB}Funcions:");
            EditorGUILayout.LabelField($"{PUNT}Play(int animacio)");
            EditorGUILayout.LabelField($"{PUNT}PlayEnrera(int animacio)");
            EditorGUILayout.LabelField($"{PUNT}Stop(int animacio)");
            EditorGUILayout.LabelField($"{PUNT}AffegirEsdeveniment(UnityAccio accio, int animacio)");
            EditorGUILayout.Space();
        }
        anuncis = Grup(anuncis, $"ANUNCIS: gestiona anuncis per plataformes mobils. {TAG_MOBIL}");
        if (anuncis)
        {
            Linia($"{PUNT}Gestiona els 3 tipus d'anuncis de Unity. Banner, imatge i Video.");
            Linia($"{PUNT}Només funciona en les plataformes: Android i iPhone");
            Linia($"{PUNT}Anuncis es Scriptable i allí s'hi posen les Id's de Android i Iphone.");
            Linia($"{PUNT}El 'test mode' només funciona a l'Editor i en 'Development Build'");
            Espai();
            Linia($"{TAB}Configuració:");
            Linia($"{PUNT}Configurar placements al Developer Dashboard. Que els noms siguin (banner, video i rewardedVideo)");
            Linia($"{PUNT}Instalar la última versió a ProjectSettings/Services/ads");
            Linia($"{PUNT}Agafar 'GameId' de ProjectSettings/Services/Ads");
            Espai();
            Linia($"{TAB}Funcions:");
            Linia($"{PUNT}Mostrar()");
            Linia($"{PUNT}El boto '_AnunciBoto', s'hi assigna automaticament la funcio al esdeveniment 'OnClick'");
            Linia($"{PUNT}El boto '_AnunciBoto', te un event a 'AnuncisPermesos', del component 'UI_Interactuable', que anula el boto.");
            Espai();
        }
        assoliments = Grup(assoliments, $"ASSOLIMENT: gestiona assoliments a les diferents plataformes. {TAG_MOBIL}{TAG_PENDENT} Falta acabar.");
        if (assoliments)
        {

        }
        audio = Grup(audio, $"AUDIO:   Crea sons. {TAG_FACILITADOR}{TAG_SCRIPTABLEOBJECT}");
        camares = Grup(camares, $"CAMARES:   Exemples programacio de cameres (millor utilitzar cinemachine). {TAG_CAMARA}{TAG_EXEMPLE}");
        cameraShake = Grup(cameraShake, $"CAMERA SHAKE:   Crea tremolors de camara. {TAG_CAMARA}{TAG_EFECTE}");
        capturar = Grup(capturar, $"CAPTURA:   Captura la pantalla en imatge o gif i comparteix depen de plataforma. {TAG_ENGAGEMENT}{TAG_PENDENT}");
        carregatZones = Grup(carregatZones, $"CARREGAT ZONTES:   Crea sons. {TAG_OPTIMITZADOR}");
        cinemachine = Grup(cinemachine, $"CINEMACHINE:   Exemples de camares. {TAG_CAMARA}{TAG_EXEMPLE}");
        clons = Grup(clons, $"CLONS:   Crea copia barata d'un GameObject de l'escena. {TAG_OPTIMITZADOR}");
        controlTemps = Grup(controlTemps, $"CONTROL TEMPS:   Crea sons. {TAG_EFECTE}");
        creadorElements = Grup(creadorElements, $"CREADOR ELEMENTS:   Crear elements facilment. {TAG_EDITOR}{TAG_FACILITADOR}{TAG_PENDENT} Fer Scriptable");
        danyable = Grup(danyable, $"DANYABLE:   Gestio rapida de vida, dany i interaccions entre ells. {TAG_GENERICS}");
        desplaçable = Grup(desplaçable, $"DESPLAÇABLE:   Gestio rapida explosió i reaccions. {TAG_EFECTE}{TAG_PENDENT} Falta acabar.");
        dialegs = Grup(dialegs, $"DIALEGS:   Sistema generic per crear dialesg. {TAG_FACILITADOR}{TAG_PENDENT} Tot i utilitzar Localization");
        direccioBasatOrientacio = Grup(direccioBasatOrientacio, $"DIRECCIO BASE ORIENTACIO:   Agafa la orientacio relativa a la direccio. {TAG_EXEMPLE}");
        escenes = Grup(escenes, $"ESCENES:   Carregador d'escenes i fade in/out. {TAG_EDITOR}{TAG_FACILITADOR}{TAG_SCRIPTABLEOBJECT}");
        esdeveniments = Grup(esdeveniments, $"ESDEVENIMENT:   Scriptables que serveixen com a canals per d'esdeveniments dinamics. {TAG_FACILITADOR}{TAG_GENERICS}{TAG_SCRIPTABLEOBJECT}");
        estadistiques = Grup(estadistiques, $"ESTADISTIQUES:   Estats de PJ: força, vida, velocitat... {TAG_GENERICS}{TAG_PENDENT} Falta acabar be, i posar scriptables");
        flocking = Grup(flocking, $"FLOKING:   Sistema de manades d'animals/coses que es mouen continuament i cohesionadament. Ex Ocells, peixos... {TAG_GENERICS}{TAG_EFECTE}{TAG_PENDENT} Acaba be-");
        FPS = Grup(FPS, $"FPS:   Mostra els fps en pantalla. {TAG_FACILITADOR}{TAG_OPTIMITZADOR}");
        Guardat = Grup(Guardat, $"GUARDAT:   Sistema de guardat dinamic. {TAG_FACILITADOR}");
        Houdini = Grup(Houdini, $"HOUDINI:   Programa modular, pot crear efectes i assets dinamics per ajudar al desenvolupament. {TAG_ASSETS}{TAG_FACILITADOR}{TAG_PENDENT}{TAG_EXEMPLE} Falta fer exemple de funcionament i provar PAckage oficial gratis.");
        icones = Grup(icones, $"ICONES:   Llista d'icones generiques. {TAG_ASSETS}");
        idiomes = Grup(idiomes, $"---IDIOMES:   Ja no es fa servir. {TAG_PENDENT}");
        iluminacio = Grup(iluminacio, $"ILUMINACIO:   Exemples bake llums. {TAG_EXEMPLE}{TAG_EFECTE}");
        localization = Grup(localization, $"LOCALITZACIO:  Sistema generic de traduccio, i tipus de fonts per TextMeshPro. {TAG_GENERICS}{TAG_ASSETS}");
        lut = Grup(lut, $"LUT:   Colorazio d'imatge amb postprocessing. {TAG_CAMARA}{TAG_ASSETS}{TAG_EFECTE}");
        maquinaEstats = Grup(maquinaEstats, $"MAQUINA ESTATS:   Sistema d'accions, transicions i condicons per crear ssitemes simples. {TAG_EDITOR}{TAG_FACILITADOR}{TAG_SCRIPTABLEOBJECT}");
        Morphing = Grup(Morphing, $"MORPHING:   Exemples funcionament del morphing. {TAG_EXEMPLE}{TAG_PENDENT} Falta: Script per controlar i Manual com crear en MAX i BLENDER.");
        Moviment3D = Grup(Moviment3D, $"MOVIMENT 3D:   Moviment basic en tres dimensions. {TAG_GENERICS}{TAG_PENDENT} Fer mes simple i modular.");
        Moviment25D = Grup(Moviment25D, $"MOVIMENT 2.5D:   Moviment en dos dimensions. {TAG_GENERICS}");
        MySQL = Grup(MySQL, $"SQL:   Base per utiltizar base de dades dins Unity. {TAG_PENDENT} Falta tot.");
        Notificacions = Grup(Notificacions, $"NOTIFICACIONS:   Avisos programables per mobil. {TAG_MOBIL}{TAG_ENGAGEMENT} Falta: fer be, mirar package, utilizar Scriptables?");
        ObjectPooling = Grup(ObjectPooling, $"OBJECT POOLING:   Intanciador optimitzat d'elements. {TAG_OPTIMITZADOR}");
        PlataformaConexio = Grup(PlataformaConexio, $"PLAT. CONEXIO:   Standard per conectarse a diverses plataformes i utilitzar les seves fucions {TAG_FACILITADOR}{TAG_MOBIL}{TAG_PENDENT} Testejar");
        PlataformaRanking = Grup(PlataformaRanking, $"PLAT. RANKING:   Standard de funcions per gestionar rankings de diverses plataformes. {TAG_FACILITADOR}{TAG_MOBIL}{TAG_PENDENT} Testejar");
        PoliticaPrivacitat = Grup(PoliticaPrivacitat, $"POLITICA PRIVACITAT: Gestio per acceptar o revocar. {TAG_MOBIL}{TAG_PENDENT} No se si es necessari ja...");
        Procedural = Grup(Procedural, $"PROCEDURAL:   Generecio d'elements aleatoriament. {TAG_EXEMPLE}{TAG_PENDENT} Fer generic");
        ProceduralAnim = Grup(ProceduralAnim, $"ANIMACIO PROCEDURAL:   Moviments programats. {TAG_EFECTE}{TAG_PENDENT} Acabar be i i walk, mirar video a VeureMesTard.");
        RagDollDinamic = Grup(RagDollDinamic, $"RAGDOLL DINAMIC:   Crea rigidbodies i joins a ossus. {TAG_EFECTE}{TAG_FACILITADOR}");
        Renderitzat = Grup(Renderitzat, $"RENDERITZAT:   Configuracions basiques de URP per tenir diferents efectes i qualitats. {TAG_EFECTE}{TAG_CAMARA}{TAG_GENERICS}{TAG_PENDENT} Netejar");
        Selector = Grup(Selector, $"SELECTOR:   Sistema basic per seleccionar elements. {TAG_GENERICS}{TAG_PENDENT} Fer-ho millor");
        ShaderLRP = Grup(ShaderLRP, $"SHADER:   LLista de shaders. {TAG_EFECTE}{TAG_ASSETS}{TAG_PENDENT} Netejar i deixar els finals");
        if (ShaderLRP)
        {
            Linia($"{TAB}AIGUA");
            Linia($"{PUNT}Base (Per fer MAR, falta canviar resolusio, posar reflexes i cubemap, provar a Terrain per aprofitar Tessellation");
            Linia($"{PUNT}LiquidAmpolla");
            Linia($"{PUNT}Moviment");
            Linia($"{PUNT}Onada (Necessita mesh amb alfa)");
            Espai();
            Linia($"{TAB}ALFA");
            Linia($"{PUNT}");
            Linia($"{PUNT}");
            Linia($"{PUNT}");
            Linia($"{PUNT}");
            Espai();
            Linia($"{TAB}");
        }
        SquashAndStretch = Grup(SquashAndStretch, $"SQUASH AND STRETCH:   Extern, estirament d'objectes dinamic. {TAG_EFECTE}");
        Temps = Grup(Temps, $"TEMPS:   Conta el temps desde l'inici del joc, el temps de sescio i entre sencions, i permet crear conometres. {TAG_EDITOR}{TAG_FACILITADOR}{TAG_PENDENT} Agregar notificacions als conometres.");
        UI = Grup(UI, $"UI:   Generic de interficie per utilitzar com a menú de joc, amb les funcions ja assignades. {TAG_GENERICS}{TAG_ASSETS}{TAG_PENDENT} Acabar tot");
        Utils = Grup(Utils, $"UTILS:   Eines que fan servir molts altres packages. {TAG_EDITOR}{TAG_FACILITADOR}");
        if (Utils)
        {
            Linia($"{TAB}EDITOR");
            Linia($"{PUNT}Aplicar presets");
            Linia($"{PUNT}Atribut [Informacio]");
            Linia($"{PUNT}Buscador referencies a UnityEvents");
            Espai();
            Linia($"{TAB}ACCIONS RAPIDES");
            Linia($"{PUNT}Destruir.");
            Linia($"{PUNT}DisableTemps.");
            Linia($"{PUNT}Mirar a camara.");
            Linia($"{PUNT}Moviment constant.");
            Linia($"{PUNT}RecordaParent.");
            Linia($"{PUNT}Seleccionar UI en enable per navegacio amb mando.");
            Linia($"{PUNT}Versio build.");
            Espai();
            Linia($"{TAB}PER UNITY EVENTS");
            Linia($"{PUNT}Debug.");
            Linia($"{PUNT}Destruir.");
            Linia($"{PUNT}DisableTemps.");
            Espai();
            Linia($"{TAB}COSES COMPLICADES");
            Linia($"{PUNT}Runtime events per ScriptableObjects. (prefab)");
            Linia($"{PUNT}Paleta de colors.");
            Linia($"{PUNT}Missatges/Debugs en Build. (prefab)");
            Linia($"{PUNT}Orientacio extrapolada direccio per anim.");
            Linia($"{PUNT}Actualitzar collider de skinnedMesh.");
            Espai();
            Linia($"{TAB}EXTENSIONS.");
            Linia($"{TAB}{TAB}Float: - float.Redueix(velocitat)");
            Linia($"{TAB}{TAB}Vector2:");
            Linia($"{TAB}{TAB}{PUNT}vector2.ToVector3 (x,0,y)");
            Linia($"{TAB}{TAB}{PUNT}vector2.EsZero");
            Linia($"{TAB}{TAB}Vector3:");
            Linia($"{TAB}{TAB}{PUNT}vector3.ToVector2 (x,z)");
            Linia($"{TAB}{TAB}{PUNT}vector3.EsZero");
            Linia($"{TAB}{TAB}Transform:");
            Linia($"{TAB}{TAB}{PUNT}transform.Direccio(objectiu)");
            Linia($"{TAB}{TAB}{PUNT}transform.Distancia(objectiu)");
            Linia($"{TAB}{TAB}Color: - color.Random()");
            Linia($"{TAB}{TAB}Debug: - new Debug.Log(Missatge) (log només en Editor)");


        }

        EditorGUILayout.EndScrollView();
    }
    bool Grup(bool grup, string descripcio)
    {
        bool trobat = false;
        for (int i = 0; i < tags.Count; i++)
        {
            if (descripcio.Contains(tags[i])) trobat = true;
        }
        return trobat ? EditorGUILayout.Foldout(grup, descripcio) : false;
        //return descripcio.Contains(TAGS[tag]) ? EditorGUILayout.Foldout(grup, descripcio) : false;
    }
    void BotoTag(string tag)
    {
        if (GUILayout.Button(tag != "" ? tag : "Tot", new GUIStyle(GUI.skin.button) { fontStyle = tags.Contains(tag) ? FontStyle.Bold : FontStyle.Normal, hover = new GUIStyleState() { textColor = tags.Contains(tag) ? Color.yellow : Color.white }, normal = new GUIStyleState() {textColor = tags.Contains(tag) ? Color.yellow : Color.white } }))
        {
            if(tag != "")
            {
                if (tags.Contains(TAGS[0])) tags.Remove(TAGS[0]);

                if (!tags.Contains(tag))
                {
                    tags.Add(tag);
                }
                else
                {
                    tags.Remove(tag);
                }
            }
            else
            {
                tags = new List<string>() { "" };
            }
        }
    }
    void Linia(string linia)
    {
        EditorGUILayout.LabelField(linia);
    }
    void Espai()
    {
        EditorGUILayout.Space();
    }
}
