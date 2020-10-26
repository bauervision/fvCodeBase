using UnityEngine;

namespace ForestVision {
	public partial class FV_Tree : MonoBehaviour {
		
		[SerializeField]
		public int _totalBranches = 10;
		public int _totalCards = 7;
		public Material _baseTrunkMat;
		public Material _baseFoliageMat;

		public int TotalBranches {
			get { return _totalBranches; }
			set { _totalBranches = value; }
		}






	}
}