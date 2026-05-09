// MediClinic - Modern Design System
// Enhanced JavaScript for interactivity and user experience

(function() {
    'use strict';

    // Initialize MediClinic
    document.addEventListener('DOMContentLoaded', function() {
        initializeUI();
        setupEventListeners();
        setupScrollAnimations();
        setupTheme();
    });

    /**
     * Initialize UI Components
     */
    function initializeUI() {
        // Initialize tooltips
        initTooltips();

        // Initialize popovers
        initPopovers();

        // Setup animations
        setupAnimations();
    }

    /**
     * Setup Bootstrap Tooltips
     */
    function initTooltips() {
        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    }

    /**
     * Setup Bootstrap Popovers
     */
    function initPopovers() {
        var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
        popoverTriggerList.map(function (popoverTriggerEl) {
            return new bootstrap.Popover(popoverTriggerEl);
        });
    }

    /**
     * Setup General Animations
     */
    function setupAnimations() {
        // Animate stat cards on view
        observeElements('[class*="stat-card"]', function(el) {
            el.style.animation = 'fadeIn 0.6s ease-out';
        });

        // Animate feature cards on view
        observeElements('.feature-card', function(el) {
            el.style.animation = 'fadeIn 0.6s ease-out';
        });
    }

    /**
     * Observe Elements for Animations
     */
    function observeElements(selector, callback) {
        if (!window.IntersectionObserver) return;

        var elements = document.querySelectorAll(selector);
        var observer = new IntersectionObserver(function(entries) {
            entries.forEach(function(entry) {
                if (entry.isIntersecting) {
                    callback(entry.target);
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.1 });

        elements.forEach(function(el) {
            observer.observe(el);
        });
    }

    /**
     * Setup Event Listeners
     */
    function setupEventListeners() {
        // Mobile nav close on link click
        setupMobileNavClose();

        // Smooth scroll for anchor links
        setupSmoothScroll();

        // Active nav link highlighting
        setupActiveNavLink();
    }

    /**
     * Mobile Navigation Close
     */
    function setupMobileNavClose() {
        var navbarCollapse = document.querySelector('.navbar-collapse');
        if (!navbarCollapse) return;

        var navLinks = navbarCollapse.querySelectorAll('.nav-link');
        navLinks.forEach(function(link) {
            link.addEventListener('click', function() {
                var bsCollapse = new bootstrap.Collapse(navbarCollapse);
                bsCollapse.hide();
            });
        });
    }

    /**
     * Smooth Scroll for Anchor Links
     */
    function setupSmoothScroll() {
        document.querySelectorAll('a[href^="#"]').forEach(function(anchor) {
            anchor.addEventListener('click', function(e) {
                var href = this.getAttribute('href');
                if (href === '#') return;

                e.preventDefault();
                var target = document.querySelector(href);
                if (target) {
                    target.scrollIntoView({ behavior: 'smooth', block: 'start' });
                }
            });
        });
    }

    /**
     * Active Navigation Link Highlighting
     */
    function setupActiveNavLink() {
        var currentLocation = location.pathname;
        var navLinks = document.querySelectorAll('.mediClinic-nav-link');

        navLinks.forEach(function(link) {
            var href = link.getAttribute('href');
            if (href && currentLocation.includes(href)) {
                link.classList.add('active');
            }
        });
    }

    /**
     * Setup Scroll Animations
     */
    function setupScrollAnimations() {
        if (!window.IntersectionObserver) return;

        var animatedElements = document.querySelectorAll('[class*="animate-"]');
        var observer = new IntersectionObserver(function(entries) {
            entries.forEach(function(entry) {
                if (entry.isIntersecting) {
                    entry.target.style.opacity = '1';
                    entry.target.style.transform = 'translateY(0)';
                }
            });
        }, { threshold: 0.1 });

        animatedElements.forEach(function(el) {
            el.style.opacity = '0';
            el.style.transform = 'translateY(10px)';
            el.style.transition = 'all 0.6s ease-out';
            observer.observe(el);
        });
    }

    /**
     * Setup Theme (Light/Dark Mode)
     */
    function setupTheme() {
        var savedTheme = localStorage.getItem('mediClinic-theme') || 'light';
        applyTheme(savedTheme);

        // Listen for system theme changes
        if (window.matchMedia) {
            window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', function(e) {
                var theme = e.matches ? 'dark' : 'light';
                applyTheme(theme);
                localStorage.setItem('mediClinic-theme', theme);
            });
        }
    }

    /**
     * Apply Theme
     */
    function applyTheme(theme) {
        if (theme === 'dark') {
            document.documentElement.setAttribute('data-bs-theme', 'dark');
        } else {
            document.documentElement.removeAttribute('data-bs-theme');
        }
    }

    /**
     * Global Utilities
     */
    window.MediClinic = {
        /**
         * Show notification toast
         */
        showNotification: function(message, type) {
            type = type || 'info';
            var toastHtml = '<div class="toast align-items-center text-white bg-' + type + '" role="alert" aria-live="assertive" aria-atomic="true">' +
                '<div class="d-flex">' +
                '<div class="toast-body">' + message + '</div>' +
                '<button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>' +
                '</div></div>';

            var container = document.querySelector('.toast-container') || createToastContainer();
            container.innerHTML = toastHtml;

            var toast = new bootstrap.Toast(container.querySelector('.toast'));
            toast.show();
        },

        /**
         * Show success notification
         */
        showSuccess: function(message) {
            this.showNotification(message, 'success');
        },

        /**
         * Show error notification
         */
        showError: function(message) {
            this.showNotification(message, 'danger');
        },

        /**
         * Show warning notification
         */
        showWarning: function(message) {
            this.showNotification(message, 'warning');
        },

        /**
         * Format date string
         */
        formatDate: function(date) {
            if (typeof date === 'string') {
                date = new Date(date);
            }
            return date.toLocaleDateString('en-US', { year: 'numeric', month: 'long', day: 'numeric' });
        }
    };

    /**
     * Create Toast Container
     */
    function createToastContainer() {
        var container = document.createElement('div');
        container.className = 'toast-container position-fixed top-0 end-0 p-3';
        container.style.zIndex = '9999';
        document.body.appendChild(container);
        return container;
    }

    /**
     * Debounce function
     */
    window.debounce = function(func, wait) {
        var timeout;
        return function executedFunction() {
            var later = function() {
                clearTimeout(timeout);
                func();
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    };

})();