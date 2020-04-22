using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Dalamud.Interface
{
    class AssetManager {
        private const string AssetStoreUrl = "https://goatcorp.github.io/DalamudAssets/";

        private static readonly Dictionary<string, string> AssetDictionary = new Dictionary<string, string> {
            {AssetStoreUrl + "UIRes/serveropcode.json", "UIRes/serveropcode.json" },
            {AssetStoreUrl + "UIRes/NotoSansCJKjp-Medium.otf", "UIRes/NotoSansCJKjp-Medium.otf" },
            {AssetStoreUrl + "UIRes/logo.png", "UIRes/logo.png" },
            {AssetStoreUrl + "UIRes/loc/dalamud/dalamud_de.json", "UIRes/loc/dalamud/dalamud_de.json" },
            {AssetStoreUrl + "UIRes/loc/dalamud/dalamud_es.json", "UIRes/loc/dalamud/dalamud_es.json" },
            {AssetStoreUrl + "UIRes/loc/dalamud/dalamud_fr.json", "UIRes/loc/dalamud/dalamud_fr.json" },
            {AssetStoreUrl + "UIRes/loc/dalamud/dalamud_it.json", "UIRes/loc/dalamud/dalamud_it.json" },
            {AssetStoreUrl + "UIRes/loc/dalamud/dalamud_ja.json", "UIRes/loc/dalamud/dalamud_ja.json" },
            {"https://img.finalfantasyxiv.com/lds/pc/global/fonts/FFXIV_Lodestone_SSF.ttf", "UIRes/gamesym.ttf" }
        };

        public static async Task EnsureAssets(string baseDir) {
            using var client = new HttpClient();

            var assetVerRemote = await client.GetStringAsync(AssetStoreUrl + "version");

            var assetVerPath = Path.Combine(baseDir, "assetver");
            var assetVerLocal = "0";
            if (File.Exists(assetVerPath))
                assetVerLocal = File.ReadAllText(assetVerPath);

            var forceRedownload = assetVerLocal != assetVerRemote;
            if (forceRedownload)
                Log.Information("Assets need redownload");

            Log.Verbose("Starting asset download");

            foreach (var entry in AssetDictionary) {
                var filePath = Path.Combine(baseDir, entry.Value);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                if (!File.Exists(filePath) || forceRedownload) {
                    Log.Verbose("Downloading {0} to {1}...", entry.Key, entry.Value);
                    try {
                        File.WriteAllBytes(filePath, await client.GetByteArrayAsync(entry.Key));
                    } catch (Exception ex) {
                        // If another game is running, we don't want to just fail in here
                        Log.Error(ex, "Could not download asset.");
                    }
                    
                }
            }

            try {
                File.WriteAllText(assetVerPath, assetVerRemote);
            } catch (Exception ex) {
                Log.Error(ex, "Could not write asset version.");
            }
        }

    }
}
