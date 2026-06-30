/* =============================================
   NOCTIS REAL STATE — site.js
   Navbar scroll effect + Scroll Reveal
   ============================================= */

(function () {
  'use strict';

  /* ── Navbar: transparente → sólida al scroll ── */
  const navbar = document.querySelector('.noctis-navbar');
  if (navbar) {
    const onScroll = () => {
      if (window.scrollY > 40) {
        navbar.classList.add('scrolled');
      } else {
        navbar.classList.remove('scrolled');
      }
    };
    window.addEventListener('scroll', onScroll, { passive: true });
    onScroll(); // estado inicial
  }

  /* ── Scroll Reveal (IntersectionObserver) ── */
  const revealEls = document.querySelectorAll('.reveal');
  if (revealEls.length > 0) {
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting) {
            entry.target.classList.add('visible');
            observer.unobserve(entry.target); // fire once
          }
        });
      },
      { threshold: 0.12, rootMargin: '0px 0px -40px 0px' }
    );
    revealEls.forEach((el) => observer.observe(el));
  }

  /* ── Auto-dismiss flash alerts después de 5 s ── */
  document.querySelectorAll('.alert-dismissible[data-auto-dismiss]').forEach((el) => {
    setTimeout(() => {
      const bsAlert = bootstrap && bootstrap.Alert ? bootstrap.Alert.getOrCreateInstance(el) : null;
      if (bsAlert) bsAlert.close();
      else el.remove();
    }, 5000);
  });

})();
