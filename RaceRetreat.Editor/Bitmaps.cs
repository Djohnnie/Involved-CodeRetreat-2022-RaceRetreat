using RaceRetreat.Domain;
using RaceRetreat.Editor.Properties;

namespace RaceRetreat.Editor;

internal static class Bitmaps
{
    private static readonly Dictionary<TileKind, Bitmap> _tileBitmaps = new Dictionary<TileKind, Bitmap>
    {
        { TileKind.R1_00, Resources.r1_00 }, { TileKind.R1_01, Resources.r1_01 },
        { TileKind.R1_02, Resources.r1_02 }, { TileKind.R1_03, Resources.r1_03 },
        { TileKind.R1_11, Resources.r1_11 }, { TileKind.R1_12, Resources.r1_12 },
        { TileKind.R1_13, Resources.r1_13 }, { TileKind.R1_14, Resources.r1_14 },
        { TileKind.R1_15, Resources.r1_15 }, { TileKind.R1_16, Resources.r1_16 },
        { TileKind.R1_17, Resources.r1_17 }, { TileKind.R1_18, Resources.r1_18 },
        { TileKind.R1_21, Resources.r1_21 }, { TileKind.R1_22, Resources.r1_22 },
        { TileKind.R1_23, Resources.r1_23 }, { TileKind.R1_24, Resources.r1_24 },
        { TileKind.R1_25, Resources.r1_25 }, { TileKind.R1_26, Resources.r1_26 },
        { TileKind.R1_27, Resources.r1_27 }, { TileKind.R1_28, Resources.r1_28 },
        { TileKind.R1_31, Resources.r1_31 }, { TileKind.R1_32, Resources.r1_32 },
        { TileKind.R1_33, Resources.r1_33 }, { TileKind.R1_34, Resources.r1_34 },
        { TileKind.R1_41, Resources.r1_41 }, { TileKind.R1_42, Resources.r1_42 },
        { TileKind.R1_43, Resources.r1_43 }, { TileKind.R1_44, Resources.r1_44 },
        { TileKind.R1_45, Resources.r1_45 }, { TileKind.R1_46, Resources.r1_46 },
        { TileKind.R1_51, Resources.r1_51 }, { TileKind.R1_52, Resources.r1_52 },
        { TileKind.R1_53, Resources.r1_53 }, { TileKind.R1_54, Resources.r1_54 },
        { TileKind.R1_55, Resources.r1_55 }, { TileKind.R1_56, Resources.r1_56 },
        { TileKind.R1_57, Resources.r1_57 }, { TileKind.R1_58, Resources.r1_58 },
        { TileKind.R1_59, Resources.r1_59 },
        { TileKind.R1_61, Resources.r1_61 }, { TileKind.R1_62, Resources.r1_62 },
        { TileKind.R1_63, Resources.r1_63 }, { TileKind.R1_64, Resources.r1_64 },
        { TileKind.R1_71, Resources.r1_71 }, { TileKind.R1_72, Resources.r1_72 },
        { TileKind.R1_73, Resources.r1_73 }, { TileKind.R1_74, Resources.r1_74 },
        { TileKind.R1_81, Resources.r1_81 }, { TileKind.R1_82, Resources.r1_82 },
        { TileKind.R1_83, Resources.r1_83 }, { TileKind.R1_84, Resources.r1_84 },
        { TileKind.R1_85, Resources.r1_85 }, { TileKind.R1_86, Resources.r1_86 },
        { TileKind.R1_87, Resources.r1_87 }, { TileKind.R1_88, Resources.r1_88 },

        { TileKind.R2_00, Resources.r2_00 }, { TileKind.R2_01, Resources.r2_01 },
        { TileKind.R2_02, Resources.r2_02 }, { TileKind.R2_03, Resources.r2_03 },
        { TileKind.R2_11, Resources.r2_11 }, { TileKind.R2_12, Resources.r2_12 },
        { TileKind.R2_13, Resources.r2_13 }, { TileKind.R2_14, Resources.r2_14 },
        { TileKind.R2_15, Resources.r2_15 }, { TileKind.R2_16, Resources.r2_16 },
        { TileKind.R2_17, Resources.r2_17 }, { TileKind.R2_18, Resources.r2_18 },
        { TileKind.R2_21, Resources.r2_21 }, { TileKind.R2_22, Resources.r2_22 },
        { TileKind.R2_23, Resources.r2_23 }, { TileKind.R2_24, Resources.r2_24 },
        { TileKind.R2_25, Resources.r2_25 }, { TileKind.R2_26, Resources.r2_26 },
        { TileKind.R2_27, Resources.r2_27 }, { TileKind.R2_28, Resources.r2_28 },
        { TileKind.R2_31, Resources.r2_31 }, { TileKind.R2_32, Resources.r2_32 },
        { TileKind.R2_33, Resources.r2_33 }, { TileKind.R2_34, Resources.r2_34 },
        { TileKind.R2_41, Resources.r2_41 }, { TileKind.R2_42, Resources.r2_42 },
        { TileKind.R2_43, Resources.r2_43 }, { TileKind.R2_44, Resources.r2_44 },
        { TileKind.R2_45, Resources.r2_45 }, { TileKind.R2_46, Resources.r2_46 },
        { TileKind.R2_51, Resources.r2_51 }, { TileKind.R2_52, Resources.r2_52 },
        { TileKind.R2_53, Resources.r2_53 }, { TileKind.R2_54, Resources.r2_54 },
        { TileKind.R2_55, Resources.r2_55 }, { TileKind.R2_56, Resources.r2_56 },
        { TileKind.R2_57, Resources.r2_57 }, { TileKind.R2_58, Resources.r2_58 },
        { TileKind.R2_59, Resources.r2_59 },
        { TileKind.R2_61, Resources.r2_61 }, { TileKind.R2_62, Resources.r2_62 },
        { TileKind.R2_63, Resources.r2_63 }, { TileKind.R2_64, Resources.r2_64 },
        { TileKind.R2_71, Resources.r2_71 }, { TileKind.R2_72, Resources.r2_72 },
        { TileKind.R2_73, Resources.r2_73 }, { TileKind.R2_74, Resources.r2_74 },
        { TileKind.R2_81, Resources.r2_81 }, { TileKind.R2_82, Resources.r2_82 },
        { TileKind.R2_83, Resources.r2_83 }, { TileKind.R2_84, Resources.r2_84 },
        { TileKind.R2_85, Resources.r2_85 }, { TileKind.R2_86, Resources.r2_86 },
        { TileKind.R2_87, Resources.r2_87 }, { TileKind.R2_88, Resources.r2_88 },

        { TileKind.R3_00, Resources.r3_00 }, { TileKind.R3_01, Resources.r3_01 },
        { TileKind.R3_02, Resources.r3_02 }, { TileKind.R3_03, Resources.r3_03 },
        { TileKind.R3_11, Resources.r3_11 }, { TileKind.R3_12, Resources.r3_12 },
        { TileKind.R3_13, Resources.r3_13 }, { TileKind.R3_14, Resources.r3_14 },
        { TileKind.R3_15, Resources.r3_15 }, { TileKind.R3_16, Resources.r3_16 },
        { TileKind.R3_17, Resources.r3_17 }, { TileKind.R3_18, Resources.r3_18 },
        { TileKind.R3_21, Resources.r3_21 }, { TileKind.R3_22, Resources.r3_22 },
        { TileKind.R3_23, Resources.r3_23 }, { TileKind.R3_24, Resources.r3_24 },
        { TileKind.R3_25, Resources.r3_25 }, { TileKind.R3_26, Resources.r3_26 },
        { TileKind.R3_27, Resources.r3_27 }, { TileKind.R3_28, Resources.r3_28 },
        { TileKind.R3_31, Resources.r3_31 }, { TileKind.R3_32, Resources.r3_32 },
        { TileKind.R3_33, Resources.r3_33 }, { TileKind.R3_34, Resources.r3_34 },
        { TileKind.R3_41, Resources.r3_41 }, { TileKind.R3_42, Resources.r3_42 },
        { TileKind.R3_43, Resources.r3_43 }, { TileKind.R3_44, Resources.r3_44 },
        { TileKind.R3_45, Resources.r3_45 }, { TileKind.R3_46, Resources.r3_46 },
        { TileKind.R3_51, Resources.r3_51 }, { TileKind.R3_52, Resources.r3_52 },
        { TileKind.R3_53, Resources.r3_53 }, { TileKind.R3_54, Resources.r3_54 },
        { TileKind.R3_55, Resources.r3_55 }, { TileKind.R3_56, Resources.r3_56 },
        { TileKind.R3_57, Resources.r3_57 }, { TileKind.R3_58, Resources.r3_58 },
        { TileKind.R3_59, Resources.r3_59 },
        { TileKind.R3_61, Resources.r3_61 }, { TileKind.R3_62, Resources.r3_62 },
        { TileKind.R3_63, Resources.r3_63 }, { TileKind.R3_64, Resources.r3_64 },
        { TileKind.R3_71, Resources.r3_71 }, { TileKind.R3_72, Resources.r3_72 },
        { TileKind.R3_73, Resources.r3_73 }, { TileKind.R3_74, Resources.r3_74 },
        { TileKind.R3_81, Resources.r3_81 }, { TileKind.R3_82, Resources.r3_82 },
        { TileKind.R3_83, Resources.r3_83 }, { TileKind.R3_84, Resources.r3_84 },
        { TileKind.R3_85, Resources.r3_85 }, { TileKind.R3_86, Resources.r3_86 },
        { TileKind.R3_87, Resources.r3_87 }, { TileKind.R3_88, Resources.r3_88 }
    };

    public static Bitmap FromTileKind(TileKind tileKind)
    {
        return _tileBitmaps[tileKind];
    }

    private static readonly Dictionary<OverlayKind, Bitmap> _overlayBitmaps = new Dictionary<OverlayKind, Bitmap>
    {
        { OverlayKind.O_00, Resources.o_00 },
        { OverlayKind.O_01, Resources.o_01 }, { OverlayKind.O_02, Resources.o_02 },
        { OverlayKind.O_03, Resources.o_03 }, { OverlayKind.O_04, Resources.o_04 },
        { OverlayKind.O_05, Resources.o_05 }, { OverlayKind.O_06, Resources.o_06 },
        { OverlayKind.O_11, Resources.o_11 }, { OverlayKind.O_12, Resources.o_12 },
        { OverlayKind.O_13, Resources.o_13 }, { OverlayKind.O_14, Resources.o_14 },
        { OverlayKind.O_21, Resources.o_21 }, { OverlayKind.O_22, Resources.o_22 },
        { OverlayKind.O_23, Resources.o_23 }
    };

    public static Bitmap FromOverlayKind(OverlayKind overlayKind)
    {
        return _overlayBitmaps[overlayKind];
    }
}