﻿using Hovercast.Core.Settings;
using Hovercast.Core.State;
using UnityEngine;

namespace Hovercast.Core.Display.Default {

	/*================================================================================================*/
	public abstract class UiBaseIconRenderer : UiSelectRenderer {

		private GameObject vIcon;

		private int vPrevTextSize;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		protected abstract Texture2D GetIconTexture();

		/*--------------------------------------------------------------------------------------------*/
		protected virtual Vector3 GetIconScale() {
			float s = vSettings.TextSize*0.75f*ArcCanvasScale;
			return new Vector3(s, s, 1);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public override void Build(ArcState pArcState, ArcSegmentState pSegState,
														float pArcAngle, ArcSegmentSettings pSettings) {
			base.Build(pArcState, pSegState, pArcAngle, pSettings);

			vIcon = GameObject.CreatePrimitive(PrimitiveType.Quad);
			vIcon.name = "Icon";
			vIcon.transform.SetParent(gameObject.transform, false);
			vIcon.GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Unlit/AlphaSelfIllum"));
			vIcon.GetComponent<Renderer>().sharedMaterial.color = Color.clear;
			vIcon.GetComponent<Renderer>().sharedMaterial.mainTexture = GetIconTexture();
			vIcon.transform.localRotation = vLabel.CanvasLocalRotation;
		}

		/*--------------------------------------------------------------------------------------------*/
		public override void Update() {
			base.Update();

			Color color = vSettings.ArrowIconColor;
			color.a *= (vSegState.HighlightProgress*0.75f + 0.25f)*vMainAlpha;

			vIcon.GetComponent<Renderer>().sharedMaterial.color = color;

			if ( vSettings.TextSize != vPrevTextSize ) {
				vPrevTextSize = vSettings.TextSize;

				float inset = vLabel.TextH;

				vLabel.SetInset(!vArcState.IsLeft, inset);

				vIcon.transform.localPosition = 
					new Vector3(0, 0, 1+(vLabel.CanvasW-inset*0.666f)*ArcCanvasScale);
				vIcon.transform.localScale = GetIconScale();
			}
		}

	}

}
