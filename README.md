# AutoFib Locked with Hotkey

<img width="891" height="886" alt="cT_cs_1184089_XAUUSD_2026-02-14_21-33-36" src="https://github.com/user-attachments/assets/5d419a8c-e200-430a-b3fe-b29a8aaa1cb2" />

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

<img width="699" height="860" alt="parameter-autofib" src="https://github.com/user-attachments/assets/b4351388-e32b-44b7-997d-409905bc5f54" />


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
