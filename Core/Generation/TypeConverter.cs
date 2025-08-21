using CS2Sharp.Core.Models;

namespace CS2Sharp.src.Generation;

public class TypeConverter
{
    private static readonly IReadOnlyDictionary<string, string> TypeMappings = new Dictionary<string, string>
    {
        // Primitive types
        ["bool"] = "bool",
        ["int"] = "int",
        ["int8"] = "sbyte",
        ["uint8"] = "byte",
        ["int16"] = "short",
        ["uint16"] = "ushort", 
        ["int32"] = "int",
        ["uint32"] = "uint",
        ["int64"] = "long",
        ["uint64"] = "ulong",
        ["float"] = "float",
        ["float32"] = "float",
        ["float64"] = "double",
        ["char"] = "char",
        
        // String types
        ["CUtlString"] = "string",
        ["CUtlStringToken"] = "string",
        ["CUtlSymbolLarge"] = "string",
        ["string_t"] = "string",
        ["char*"] = "string",
        
        // Vector & Angle types
        ["Vector"] = "Vector3",
        ["Vector3"] = "Vector3", 
        ["Vector2D"] = "Vector2",
        ["Vector4D"] = "Vector4",
        ["QAngle"] = "Vector3",
        ["Quaternion"] = "Quaternion",
        
        // Handle types (CS2 specific)
        ["CHandle<CBaseEntity>"] = "nint",
        ["CHandle<CCSPlayerPawn>"] = "nint",
        ["CHandle<CCSPlayerController>"] = "nint",
        ["CHandle<CBasePlayerController>"] = "nint",
        ["CHandle<C_BasePlayerWeapon>"] = "nint",
        ["CHandle<CBasePlayerWeapon>"] = "nint",
        ["CHandle<CCSWeaponBase>"] = "nint",
        ["CHandle<C_BaseModelEntity>"] = "nint",
        ["CHandle<C_BaseEntity>"] = "nint",
        ["CHandle<CFogController>"] = "nint",
        ["CHandle<CColorCorrection>"] = "nint",
        ["CHandle<CTonemapController2>"] = "nint",
        ["CHandle<C_PostProcessingVolume>"] = "nint",
        ["CHandle<CInfoFan>"] = "nint",
        ["CHandle<C_EconWearable>"] = "nint",
        ["CHandle<C_BasePropDoor>"] = "nint",
        ["CHandle<C_BaseFlex>"] = "nint",
        ["EHANDLE"] = "nint",
        ["CEntityHandle"] = "nint",
        ["CEntityIndex"] = "int",
        
        // Color types
        ["Color"] = "Color",
        ["RGBA"] = "Color",
        
        // CS2 Game-specific types
        ["GameTime_t"] = "float",
        ["GameTick_t"] = "int",
        ["XUID"] = "ulong",
        ["CPlayerSlot"] = "int",
        ["WorldGroupId_t"] = "int",
        
        // Strong handles/resources
        ["HModelStrong"] = "nint",
        ["HMaterialStrong"] = "nint", 
        ["HRenderTextureStrong"] = "nint",
        ["HParticleSystemDefinitionStrong"] = "nint",
        ["HSequence"] = "int",
        ["HPostProcessingStrong"] = "nint",
        
        // Enums (based on naming patterns from dump)
        ["MeshGroupMask_t"] = "uint",
        ["ButtonBitMask_t"] = "uint",
        ["TakeDamageFlags_t"] = "uint",
        ["EntityPlatformTypes_t"] = "byte",
        ["EntitySubclassID_t"] = "uint",
        ["MoveCollide_t"] = "byte",
        ["MoveType_t"] = "byte",
        ["BloodType"] = "int",
        ["RenderMode_t"] = "byte",
        ["RenderFx_t"] = "byte",
        ["EntityRenderAttribute_t"] = "byte",
        ["DecalMode_t"] = "int",
        ["DoorState_t"] = "int",
        ["BeamType_t"] = "int",
        ["BeamClipStyle_t"] = "int",
        ["AttachmentHandle_t"] = "byte",
        ["loadout_slot_t"] = "int",
        ["CSPlayerBlockingUseAction_t"] = "int",
        ["PredictedDamageTag_t"] = "int",
        ["CSPlayerState"] = "int",
        ["CSWeaponMode"] = "int",
        ["WeaponGameplayAnimState"] = "int",
        ["item_definition_index_t"] = "ushort",
        ["EntityDisolveType_t"] = "int",
        ["TimelineCompression_t"] = "int",
        ["ValueRemapperInputType_t"] = "int",
        ["ValueRemapperOutputType_t"] = "int",
        ["ValueRemapperHapticsType_t"] = "int",
        ["ValueRemapperMomentumType_t"] = "int",
        ["ValueRemapperRatchetType_t"] = "int",
        ["PointWorldTextJustifyHorizontal_t"] = "int",
        ["PointWorldTextJustifyVertical_t"] = "int",
        ["PointWorldTextReorientMode_t"] = "int",
        ["WeaponPurchaseCount_t"] = "byte",
        ["FixAngleSet_t"] = "int",
        ["ShardSolid_t"] = "byte",
        
        // Complex CS2 types (treated as pointers/handles)
        ["CPathQueryComponent::Storage_t"] = "nint",
        ["fogplayerparams_t"] = "nint",
        ["audioparams_t"] = "nint", 
        ["CountdownTimer"] = "nint",
        ["CEconItemView"] = "nint",
        ["CModelState"] = "nint",
        ["CBodyComponent::Storage_t"] = "nint",
        ["CRenderComponent::Storage_t"] = "nint",
        ["CHitboxComponent::Storage_t"] = "nint",
        ["CDestructiblePartsSystemComponent*"] = "nint",
        ["CCollisionProperty"] = "nint",
        ["CGlowProperty"] = "nint",
        ["CPlayer_WeaponServices*"] = "nint",
        ["CPlayer_ItemServices*"] = "nint",
        ["CPlayer_AutoaimServices*"] = "nint",
        ["CPlayer_ObserverServices*"] = "nint",
        ["CPlayer_WaterServices*"] = "nint",
        ["CPlayer_UseServices*"] = "nint",
        ["CPlayer_FlashlightServices*"] = "nint",
        ["CPlayer_CameraServices*"] = "nint",
        ["CPlayer_MovementServices*"] = "nint",
        ["ViewAngleServerChange_t"] = "nint",
        ["sky3dparams_t"] = "nint",
        ["CPropDataComponent::Storage_t"] = "nint",
        ["EntitySpottedState_t"] = "nint",
        ["CCSGameModeRules*"] = "nint",
        ["CRetakeGameRules"] = "nint",
        ["C_CSGameRules*"] = "nint",
        ["CCSPlayer_BulletServices*"] = "nint",
        ["CCSPlayer_HostageServices*"] = "nint",
        ["CCSPlayer_BuyServices*"] = "nint",
        ["CCSPlayer_GlowServices*"] = "nint",
        ["CCSPlayer_ActionTrackingServices*"] = "nint",
        ["CCSPlayer_PingServices*"] = "nint",
        ["CAttributeContainer"] = "nint",
        ["CAttributeList"] = "nint",
        ["CEnvWindShared"] = "nint",
        ["fogparams_t"] = "nint",
        ["CLightComponent::Storage_t"] = "nint",
        ["shard_model_desc_t"] = "nint",
        ["SoundeventPathCornerPairNetworked_t"] = "nint",
        ["CTransform"] = "nint",
    };

    private static readonly IReadOnlyList<string> KnownComplexTypes = new[]
    {
        "CUtlVector", "CNetworkUtlVectorBase", "C_UtlVectorEmbeddedNetworkVar"
    };
    
    public static string AsCSharpType(string originalType)
    {
        if (string.IsNullOrWhiteSpace(originalType))
            return "object";
        
        string cleanType = originalType.Trim();
        
        if (IsArrayType(cleanType))
            return ConvertArrayType(cleanType);
        
        if (IsPointerType(cleanType))
            return ConvertPointerType(cleanType);
        
        if (IsTemplateType(cleanType))
            return ConvertTemplateType(cleanType);
        
        if (TypeMappings.TryGetValue(cleanType, out string? mappedType))
            return mappedType;
        
        if (IsKnownComplexType(cleanType))
            return "nint";
        
        return ConvertUnknownType(cleanType);
    }
    
    public static string GetOriginalTypeName(string originalType) => originalType?.Trim() ?? "unknown";

    private static bool IsArrayType(string type)
    {
        return type.Contains('[') && type.Contains(']') || 
               type.StartsWith("CUtlVector<") ||
               type.StartsWith("CNetworkUtlVectorBase<");
    }

    private static string ConvertArrayType(string type)
    {
        if (type.Contains('[') && type.Contains(']'))
        {
            // C-style arrays: int[32] -> int[]
            string baseType = type.Substring(0, type.IndexOf('['));
            return AsCSharpType(baseType) + "[]";
        }
        
        if (type.StartsWith("CUtlVector<"))
        {
            // CUtlVector<T> -> T[]
            string innerType = ExtractTemplateParameter(type);
            return AsCSharpType(innerType) + "[]";
        }
        
        if (type.StartsWith("CNetworkUtlVectorBase<"))
        {
            // CNetworkUtlVectorBase<T> -> T[]
            string innerType = ExtractTemplateParameter(type);
            return AsCSharpType(innerType) + "[]";
        }
        
        return "nint";
    }

    private static bool IsPointerType(string type)
    {
        return type.EndsWith('*') || type.Contains("Ptr<");
    }

    private static string ConvertPointerType(string type)
    {
        if (type.EndsWith('*'))
        {
            string baseType = type.TrimEnd('*').Trim();
            if (baseType == "char" || baseType == "const char")
                return "string";
                
            return "nint";
        }
        
        if (type.Contains("Ptr<"))
        {
            return "nint";
        }
        
        return "nint";
    }

    private static bool IsTemplateType(string type)
    {
        return type.Contains('<') && type.Contains('>') && !IsArrayType(type);
    }

    private static string ConvertTemplateType(string type)
    {
        if (type.StartsWith("CHandle<"))
            return "nint";
            
        if (type.StartsWith("CStrongHandle<"))
            return "nint";
            
        if (type.StartsWith("CWeakHandle<"))
            return "nint";
        
        return "nint";
    }

    private static bool IsKnownComplexType(string type)
    {
        return KnownComplexTypes.Contains(type) || 
               type.StartsWith('C') && char.IsUpper(type.Skip(1).FirstOrDefault());
    }

    private static string ConvertUnknownType(string type)
    {
        if (type.EndsWith("_t") || type.StartsWith('E') && char.IsUpper(type.Skip(1).FirstOrDefault()))
            return "int";
        
        if (type.Contains("Flags") || type.Contains("FLAGS"))
            return "uint";
        
        if (type.StartsWith('C') && char.IsUpper(type.Skip(1).FirstOrDefault()))
            return "nint";
        
        return "int";
    }

    private static string ExtractTemplateParameter(string templateType)
    {
        int startIndex = templateType.IndexOf('<') + 1;
        int endIndex = templateType.LastIndexOf('>');
        
        if (startIndex > 0 && endIndex > startIndex)
        {
            return templateType.Substring(startIndex, endIndex - startIndex).Trim();
        }
        
        return "object";
    }
}