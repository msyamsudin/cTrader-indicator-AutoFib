# AutoFib Locked with Hotkey
Indikator Fibonacci otomatis untuk cTrader dengan klik mouse untuk menentukan level harga dan shortcut kunci level.

## Fitur
- Set Low: Klik kiri biasa di chart
- Set High: Ctrl + Klik kiri di chart
- Toggle kunci/unlock: Tekan tombol **K**
- Saat terkunci: klik mouse diabaikan, level tidak bisa diubah manual
- Otomatis update higher high / lower low baru meski terkunci
- Warna garis berubah lebih gelap saat terkunci (untuk membedakan status)
- Custom level Fibonacci (maksimal 5 level)
  - Bisa aktifkan/matiin per level
  - Nilai level bebas (retracement, extension, negatif)
  - Warna per level via color picker
- Label nilai level (%) di sisi kanan garis (bisa dimatikan)
- Nilai Low/High bertahan meski ubah parameter atau toggle lock

## Cara Pakai
1. Attach indikator ke chart
2. Klik kiri → set Low
3. Ctrl + Klik kiri → set High
   → Garis fib muncul otomatis
4. Tekan **K** → kunci level (klik mouse diabaikan)
5. Tekan **K** lagi → buka kunci
6. Ubah warna, style, thickness, atau custom level di parameter
7. Untuk reset total: remove indikator dari chart, lalu attach ulang

## Parameter Utama
- High / 50% / Low Color (Unlocked) → warna saat tidak terkunci
- Line Style & Thickness
- Show Level Labels → tampilkan % di sisi garis
- Custom Levels 1–5 → enable, value, color

## Catatan
- Garis Low (0%) dan High (100%) selalu aktif
- Level custom bisa overlap (misal 50% di custom level 4)
- Hotkey 'K' bisa diganti di kode (baris Initialize)

Build di cTrader Automate → attach → selesai.
