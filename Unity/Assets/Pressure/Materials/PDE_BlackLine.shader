Shader "Zbuffers/UnlitAdditiveBlackLine" {
	
	// A simple shader to draw lines 
	// (backface culling and lightning are turned off)
	
    Properties {
    	_Color ("Main Color", Color) = (0,0,0,1)
    }
    
    SubShader {
        Pass {
        	Blend SrcAlpha OneMinusSrcAlpha 
            ZWrite Off
        	Color [_Color]
            Lighting Off
            Cull Off
        }
        
        /* Pass {
        	Blend SrcAlpha OneMinusSrcAlpha 
            ZWrite Off
        	Color [_BackColor]
            Lighting Off
            Cull Front  
        } */
    }
    
    FallBack "VertexLit"
} 