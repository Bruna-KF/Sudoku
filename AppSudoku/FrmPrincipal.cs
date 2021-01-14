using System;
using System.Drawing;
using System.Windows.Forms;
using VisualArrays;

namespace AppSudoku
{
    public partial class FrmPrincipal : Form
    {
        /// <summary>
        /// NOTIONS :
        ///  - Copier des éléments d'un tableau 2D dans une grille
        ///  - Désactiver des cellules de la grille
        ///  - Utiliser une valeur spéciale dont l'apparence est différente des autres valeurs
        ///  - Parcourir les cellules d'une grille en deux dimensions
        ///     - Parcourir des rangées, des colonnes ou des zones de 3 X 3
        ///  - Utiliser un tableau de booléens pour marquer les nombres utilisés.
        /// </summary>

        public const string APP_INFO = "(Matériel)";

        #region Sudoku Moyen (SEUL LE MOYEN EST UTILISÉ)
        int[,] m_tabSudokuMoyen =
        {
            {-5,2,4,3,1,7,-8,6,-9},
            {3,1,7,-6,-8,-9,4,5,2},
            {-8,-9,6,4,-2,5,1,-3,7},
            {2,8,1,-9,3,-6,5,-7,4},
            {-4,6,-5,8,-7,2,-3,9,-1},
            {9,-7,3,-5,4,-1,6,2,8},
            {7,-5,8,1,-9,3,2,-4,-6},
            {6,4,9,-2,-5,-8,7,1,3},
            {-1,3,-2,7,6,4,9,8,-5}
        };
        #region Sudoku Facile
        int[,] m_tabSudokuFacile = {
            {5,1,-2,-4,8,6,9,-7,-3},
            {6,-7,-9,2,3,-1,-5,-4,8},
            {-8,4,3,-5,9,-7,2,6,-1},
            {2,3,5,7,1,8,4,9,-6},
            {7,-9,1,-3,-6,-4,8,-2,5},
            {-4,6,8,9,5,2,3,1,7},
            {-1,5,7,-8,2,-9,6,3,-4},
            {3,-2,-6,-1,4,5,-7,8,9},
            {-9,-8,4,6,7,-3,-1,5,2},
        };
        #endregion
        #region Sudoku Difficile
        int[,] m_tabSudokuDifficile =
        {
            {5,9,6,-2,7,-4,-8,1,3},
            {-7,2,8,1,3,9,6,-4,-5},
            {4,-3,1,5,8,-6,-2,7,-9},
            {-9,4,3,-6,-1,2,-5,8,7},
            {-8,5,2,3,9,7,1,6,-4},
            {6,1,-7,4,-5,-8,9,3,-2},
            {-3,8,-4,-9,6,5,7,-2,1},
            {-1,-7,5,8,2,3,4,9,-6},
            {2,6,-9,-7,4,-1,3,5,8 }
        };
        #endregion
        #endregion

        #region NE PAS MODIFIER
        public FrmPrincipal()
        {
            InitializeComponent();
            Text += APP_INFO;
            PréparerNouvellePartie();
        }
        /// <summary>
        /// Démarre une nouvelle partie
        /// </summary>
        private void mnuFichierNouvellePartie_Click(object sender, EventArgs e)
        {
            PréparerNouvellePartie();
        }
        //================================================================================================
        private void mnuFichierQuitter_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region PréparerNouvellePartie

        private void PréparerNouvellePartie()
        {
            
            mnuAideSolutionner.Enabled = true;
            viaGrille.Enabled = true;
            viaGrille.Clear();

            for (int rangée = 0; rangée < viaGrille.RowCount; rangée++)
            {
                for(int colonne = 0; colonne < viaGrille.ColumnCount; colonne++)
                {
                    if (m_tabSudokuMoyen[rangée,colonne] < 0)
                    {
                        viaGrille[rangée, colonne] = -m_tabSudokuMoyen[rangée, colonne];
                        
                        viaGrille.DisableCell(rangée,colonne);
                    }
                }
            }
            
        }
        #endregion

        #region Solutionner le Sudoku

        private void mnuAideSolutionner_Click(object sender, EventArgs e)
        {
            

            for (int rangée = 0; rangée < viaGrille.RowCount; rangée++)
            {
                for (int colonne = 0; colonne < viaGrille.ColumnCount; colonne++)
                {
                    if (m_tabSudokuMoyen[rangée, colonne] > 0)
                    {
                        viaGrille[rangée, colonne] = m_tabSudokuMoyen[rangée, colonne];
                        viaGrille.SelectedIndex = -1;
                        
                    }
                }
                
            }
            mnuAideSolutionner.Enabled = false;
            panJeu.BackColor = Color.Orange;
            viaGrille.Enabled = false;
            
        }
        #endregion

        #region VérifierLaFinDeLaPartie

        private void VérifierLaFinDeLaPartie()
        {
            int cptZéro = 0;
            for (int rangée = 0; rangée < viaGrille.RowCount; rangée++)
            {
                for (int colonne = 0; colonne < viaGrille.ColumnCount; colonne++)
                {
                    if(viaGrille[rangée,colonne] == 0)
                    {
                        cptZéro++;
                    }
                }

            }
            if (cptZéro == 0 && panJeu.BackColor == Color.Green)
            {
                panJeu.BackColor = Color.Orange;
                viaGrille.Enabled = false;
            }

        }
        #endregion

        #region Vérifier l'intégrité du Sudoku

        private void VérifierToutesLesRangées()
        {
            

            for (int rangée = 0; rangée < viaGrille.RowCount; rangée++)
            {
                bool[] tabDesChiffresPrésents = { false, false, false, false, false, false, false, false, false, false };
         
                for (int colonne = 0; colonne < viaGrille.ColumnCount; colonne++)
                {
                    int chiffreCourant = viaGrille[rangée,colonne];
                    if (chiffreCourant != 0)
                    {
                        
                        if (tabDesChiffresPrésents[chiffreCourant] == false)
                        {
                            tabDesChiffresPrésents[chiffreCourant] = true; 
                        }
                        else
                        {
                            panJeu.BackColor = Color.Red;
                        }
                        
                    }
                    
                }
            }
        }

        private void VérifierToutesLesColonnes()
        {

            for (int colonne = 0; colonne < viaGrille.ColumnCount; colonne++)
            {

                bool[] tabDesChiffresPrésents = { false, false, false, false, false, false, false, false, false, false };

                for (int rangée = 0; rangée < viaGrille.RowCount; rangée++)
                { 
                    
                    int chiffreCourant = viaGrille[rangée, colonne];
                    if (chiffreCourant != 0)
                    {
                        if (tabDesChiffresPrésents[chiffreCourant] == false)
                        {
                            tabDesChiffresPrésents[chiffreCourant] = true;
                        }
                        else
                        {
                            panJeu.BackColor = Color.Red;
                        }
                    }

                }
            }

        }

        private void VérifierToutesLesRegions()
        {
			// parcourir les régions 
			for (int rangeeR = 0; rangeeR < 9; rangeeR+=3)
            {
                for (int colonneR = 0; colonneR < 9; colonneR+=3)
                {
					//  initialiser le tableau des chiffres à false
                    bool[] tabDesChiffresPrésents = { false, false, false, false, false, false, false, false, false, false };
					
					//  parcourir l'intérieur de chaque des régions
                    for (int rangee = rangeeR; rangee < rangeeR + 3; rangee++)
					{	
                        for (int colonne = colonneR; colonne < colonneR + 3; colonne++)
                        {
							//  prendre le chiffre courant dans la grille
                            int chiffreCourant = viaGrille[rangee, colonne];
                            //  vérifier l'intégrité
                            if (chiffreCourant != 0)
                            {   
                                if (tabDesChiffresPrésents[chiffreCourant] == false)
                                {
                                    tabDesChiffresPrésents[chiffreCourant] = true;
                                }
                                else
                                {
                                    panJeu.BackColor = Color.Red;
                                }
                            }
                        }
					}
                }
            }

        }
        #endregion

        #region viaGrille_ValueChanged (À CONSULTER/NE PAS MODIFIER)
        //================================================================================================
        /// <summary>
        /// Vérifie l'intégrité du jeu et si la partie est terminée à chaque changement de valeur dans la grille.
        /// </summary>
        private void viaGrille_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            panJeu.BackColor = Color.Green;
            VérifierToutesLesColonnes();
            VérifierToutesLesRangées();
            VérifierToutesLesRegions();
            VérifierLaFinDeLaPartie();
        }
        #endregion

      
    }
}
