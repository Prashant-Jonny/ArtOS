﻿using Hovercast.Core.Settings;
using Hovercast.Core.State;
using UnityEngine;

namespace Hovercast.Core.Display.Default {

	/*================================================================================================*/
	public abstract class UiBaseToggleRenderer : UiSelectRenderer {

		private GameObject vOuter;
		private GameObject vInner;

		private int vPrevTextSize;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		protected abstract Texture2D GetOuterTexture();

		/*--------------------------------------------------------------------------------------------*/
		protected abstract Texture2D GetInnerTexture();

		/*--------------------------------------------------------------------------------------------*/
		protected abstract bool IsToggled();


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public override void Build(ArcState pArcState, ArcSegmentState pSegState,
														float pArcAngle, ArcSegmentSettings pSettings) {
			base.Build(pArcState, pSegState, pArcAngle, pSettings);

			vOuter = GameObject.CreatePrimitive(PrimitiveType.Quad);
			vOuter.name = "ToggleOuter";
			vOuter.transform.SetParent(gameObject.transform, false);
			vOuter.GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Unlit/AlphaSelfIllum"));
			vOuter.GetComponent<Renderer>().sharedMaterial.color = Color.clear;
			vOuter.GetComponent<Renderer>().sharedMaterial.mainTexture = GetOuterTexture();
			vOuter.transform.localRotation = vLabel.CanvasLocalRotation;

			vInner = GameObject.CreatePrimitive(PrimitiveType.Quad);
			vInner.name = "ToggleInner";
			vInner.transform.SetParent(gameObject.transform, false);
			vInner.GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Unlit/AlphaSelfIllum"));
			vInner.GetComponent<Renderer>().sharedMaterial.color = Color.clear;
			vInner.GetComponent<Renderer>().sharedMaterial.mainTexture = GetInnerTexture();
			vInner.transform.localRotation = vLabel.CanvasLocalRotation;
		}

		/*--------------------------------------------------------------------------------------------*/
		public override void Update() {
			base.Update();

			Color color = vSettings.ToggleIconColor;
			color.a *= (vSegState.HighlightProgress*0.25f + 0.75f)*vMainAlpha;

			vOuter.GetComponent<Renderer>().sharedMaterial.color = color;
			vInner.GetComponent<Renderer>().sharedMaterial.color = color;
			vInner.GetComponent<Renderer>().enabled = IsToggled();

			if ( vSettings.TextSize != vPrevTextSize ) {
				vPrevTextSize = vSettings.TextSize;

				float inset = vLabel.TextH*0.85f;
				Vector3 pos = new Vector3(0, 0, 1+inset*0.75f*ArcCanvasScale);
				Vector3 scale = Vector3.one*(vSettings.TextSize*0.75f*ArcCanvasScale);

				vLabel.SetInset(vArcState.IsLeft, inset);

				vOuter.transform.localPosition = pos;
				vOuter.transform.localScale = scale;

				vInner.transform.localPosition = pos;
				vInner.transform.localScale = scale;
			}
		}

	}

}
