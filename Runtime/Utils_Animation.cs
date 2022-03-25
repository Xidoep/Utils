using UnityEngine;

namespace XS_Utils
{
    public static class XS_Animation
    {
        /// <summary>
        /// Retorna una extrapolacio de en quina direccio s'hauria d'orientar l'animacio, 
        /// en base al la direccio del moviment i la orientacio a la que mira. 
        /// (Especial per personatges que es poden moure i mirar en direccions diferents).
        /// </summary>
        /// <param name="transform">El transform que es mou, (només s'utilitza per treuren el transform.right)</param>
        /// <param name="orientacio">Cap a on mira</param>
        /// <param name="direccio">Cap a on es mou</param>
        /// <param name="debug">Debugs. blau = Direccio. Vermell = Orientacio, Groc = Animacio</param>
        /// <returns></returns>
        public static Vector2 ExtrapolarDireccioAnimacioSegonsOrientacio(this Transform transform, Vector2 orientacio,
            Vector2 direccio, bool debug = false)
        {
            Vector2 _tmp;
            if (direccio != Vector2.zero)
            {
                _tmp = Vector3.forward.ToVector2() * (Vector3.Dot(orientacio, direccio)) +
                    Vector3.right.ToVector2() * (Vector3.Dot(transform.right.ToVector2(), direccio));
            }
            else
            {
                _tmp = new Vector2(0, 1);
            }


            if (debug)
            {
                Debugar.DrawRay(transform.position, direccio.ToVector3_Pla(), Color.blue, 0.01f);

                if (orientacio != Vector2.zero)
                {
                    Debugar.DrawRay(transform.position, orientacio.ToVector3_Pla(), Color.red, 0.01f);
                }

                Debugar.DrawRay(transform.position, _tmp.ToVector3_Pla(), Color.yellow, 0.01f);
            }

            return _tmp;

        }

        public static int ToHash(this string name) => Animator.StringToHash(name);
        ///LLEGIR CUSTOM CURVE
        ///Si crees un parametre al animator amb el mateix nom que la corva, ho captura automaticament.
        ///Aixì ja ho pots agafar amb animator.GetFloat(nom);
    }
}

