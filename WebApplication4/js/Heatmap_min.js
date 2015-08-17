﻿(function (n) {
    var j = function (a) { var e = { data: [], heatmap: a }; this.max = 1; this.get = function (a) { return e[a] }; this.set = function (a, f) { e[a] = f } }; j.prototype = {
        addDataPoint: function (a, e) { if (!(0 > a || 0 > e)) { var g = this.get("heatmap"), f = this.get("data"); f[a] || (f[a] = []); f[a][e] || (f[a][e] = 0); f[a][e] += 3 > arguments.length ? 1 : arguments[2]; this.set("data", f); this.max < f[a][e] ? (g.get("actx").clearRect(0, 0, g.get("width"), g.get("height")), this.setDataSet({ max: f[a][e], data: f }, !0)) : g.drawAlpha(a, e, f[a][e], !0) } }, setDataSet: function (a,
e) { var g = this.get("heatmap"), f = [], h = a.data, c = h.length; g.clear(); this.max = a.max; g.get("legend") && g.get("legend").update(a.max); if (null != e && e) for (var b in h) { if (void 0 !== b) for (var i in h[b]) void 0 !== i && g.drawAlpha(b, i, h[b][i], !1) } else for (; c--;) b = h[c], g.drawAlpha(b.x, b.y, b.count, !1), f[b.x] || (f[b.x] = []), f[b.x][b.y] || (f[b.x][b.y] = 0), f[b.x][b.y] = b.count; g.colorize(); this.set("data", h) }, exportDataSet: function () {
    var a = this.get("data"), e = [], g; for (g in a) if (void 0 !== g) for (var f in a[g]) void 0 !== f && e.push({
        x: parseInt(g,
        10), y: parseInt(f, 10), count: a[g][f]
    }); return { max: this.max, data: e }
}, generateRandomDataSet: function (a) { var e = this.get("heatmap"), g = e.get("width"), e = e.get("height"), f = {}, b = Math.floor(1E3 * Math.random() + 1); f.max = b; for (var c = []; a--;) c.push({ x: Math.floor(Math.random() * g + 1), y: Math.floor(Math.random() * e + 1), count: Math.floor(Math.random() * b + 1) }); f.data = c; this.setDataSet(f) }
    }; var c = function (a) {
        this.config = a; var e = { element: null, labelsEl: null, gradientCfg: null, ctx: null }; this.get = function (a) { return e[a] }; this.set =
        function (a, f) { e[a] = f }; this.init()
    }; c.prototype = {
        init: function () {
            var a = this.config, e = a.title || "Legend", g = a.position, f = a.offset || 10, a = document.createElement("ul"), b = ""; this.processGradientObject(); b = -1 < g.indexOf("t") ? b + ("top:" + f + "px;") : b + ("bottom:" + f + "px;"); b = -1 < g.indexOf("l") ? b + ("left:" + f + "px;") : b + ("right:" + f + "px;"); g = document.createElement("div"); g.style.cssText = "border-radius:5px;position:absolute;" + b + "font-family:Helvetica; width:256px;z-index:10000000000; background:rgba(255,255,255,1);padding:10px;border:1px solid black;margin:0;";
            g.innerHTML = "<h3 style='padding:0;margin:0;text-align:center;font-size:16px;'>" + e + "</h3>"; a.style.cssText = "position:relative;font-size:12px;display:block;list-style:none;list-style-type:none;margin:0;height:15px;"; e = document.createElement("div"); e.style.cssText = ["position:relative;display:block;width:256px;height:15px;border-bottom:1px solid black; background-image:url(", this.createGradientImage(), ");"].join(""); g.appendChild(a); g.appendChild(e); this.set("element", g); this.set("labelsEl", a); this.update(1)
        },
        processGradientObject: function () { var a = this.config.gradient, e = [], g; for (g in a) a.hasOwnProperty(g) && e.push({ stop: g, value: a[g] }); e.sort(function (a, e) { return a.stop - e.stop }); e.unshift({ stop: 0, value: "rgba(0,0,0,0)" }); this.set("gradientArr", e) }, createGradientImage: function () {
            var a = this.get("gradientArr"), e = a.length, g = document.createElement("canvas"), f = g.getContext("2d"), b; g.width = "256"; g.height = "15"; b = f.createLinearGradient(0, 5, 256, 10); for (var c = 0; c < e; c++) b.addColorStop(1 / (e - 1) * c, a[c].value); f.fillStyle =
            b; f.fillRect(0, 5, 256, 10); f.strokeStyle = "black"; f.beginPath(); for (c = 0; c < e; c++) f.moveTo((256 * 1 / (e - 1) * c >> 0) + 0.5, 0), f.lineTo((256 * 1 / (e - 1) * c >> 0) + 0.5, 0 == c ? 15 : 5); f.moveTo(255.5, 0); f.lineTo(255.5, 15); f.moveTo(255.5, 4.5); f.lineTo(0, 4.5); f.stroke(); this.set("ctx", f); return g.toDataURL()
        }, getElement: function () { return this.get("element") }, update: function (a) {
            for (var e = this.get("gradientArr"), g = this.get("ctx"), b = this.get("labelsEl"), c, k = "", m, i = 0; i < e.length; i++) c = a * e[i].stop >> 0, m = g.measureText(c).width / 2 >> 0, 0 ==
            i && (m = 0), i == e.length - 1 && (m *= 2), k += '<li style="position:absolute;left:' + (((256 * 1 / (e.length - 1) * i || 0) >> 0) - m + 0.5) + 'px">' + c + "</li>"; b.innerHTML = k
        }
    }; var b = function (a) { var e = { radius: 40, element: {}, canvas: {}, acanvas: {}, ctx: {}, actx: {}, legend: null, visible: !0, width: 0, height: 0, max: !1, gradient: !1, opacity: 180, premultiplyAlpha: !1, bounds: { l: 1E3, r: 0, t: 1E3, b: 0 }, debug: !1 }; this.store = new j(this); this.get = function (a) { return e[a] }; this.set = function (a, b) { e[a] = b }; this.configure(a); this.init() }; b.prototype = {
        configure: function (a) {
            this.set("radius",
            a.radius || 40); this.set("element", a.element instanceof Object ? a.element : document.getElementById(a.element)); this.set("visible", null != a.visible ? a.visible : !0); this.set("max", a.max || !1); this.set("gradient", a.gradient || { "0.45": "rgb(0,0,255)", "0.55": "rgb(0,255,255)", "0.65": "rgb(0,255,0)", "0.95": "yellow", 1: "rgb(255,0,0)" }); this.set("opacity", parseInt(255 / (100 / a.opacity), 10) || 180); this.set("width", a.width || 0); this.set("height", a.height || 0); this.set("debug", a.debug); a.legend && (a = a.legend, a.gradient = this.get("gradient"),
            this.set("legend", new c(a)))
        }, resize: function () { var a = this.get("element"), e = this.get("canvas"), b = this.get("acanvas"); e.width = b.width = this.get("width") || a.style.width.replace(/px/, "") || this.getWidth(a); this.set("width", e.width); e.height = b.height = this.get("height") || a.style.height.replace(/px/, "") || this.getHeight(a); this.set("height", e.height) }, init: function () {
            var a = document.createElement("canvas"), e = document.createElement("canvas"), b = a.getContext("2d"), f = e.getContext("2d"), c = this.get("element"); this.initColorPalette();
            this.set("canvas", a); this.set("ctx", b); this.set("acanvas", e); this.set("actx", f); this.resize(); a.style.cssText = e.style.cssText = "position:absolute;top:0;left:0;z-index:10000000;"; this.get("visible") || (a.style.display = "none"); c.appendChild(a); this.get("legend") && c.appendChild(this.get("legend").getElement()); this.get("debug") && document.body.appendChild(e); f.shadowOffsetX = 15E3; f.shadowOffsetY = 15E3; f.shadowBlur = 15
        }, initColorPalette: function () {
            var a = document.createElement("canvas"), e = this.get("gradient"),
            b, c; a.width = "1"; a.height = "256"; a = a.getContext("2d"); b = a.createLinearGradient(0, 0, 1, 256); c = a.getImageData(0, 0, 1, 1); c.data[0] = c.data[3] = 64; c.data[1] = c.data[2] = 0; a.putImageData(c, 0, 0); c = a.getImageData(0, 0, 1, 1); this.set("premultiplyAlpha", 60 > c.data[0] || 70 < c.data[0]); for (var h in e) b.addColorStop(h, e[h]); a.fillStyle = b; a.fillRect(0, 0, 1, 256); this.set("gradient", a.getImageData(0, 0, 1, 256).data)
        }, getWidth: function (a) {
            var e = a.offsetWidth; a.style.paddingLeft && (e += a.style.paddingLeft); a.style.paddingRight && (e +=
            a.style.paddingRight); return e
        }, getHeight: function (a) { var e = a.offsetHeight; a.style.paddingTop && (e += a.style.paddingTop); a.style.paddingBottom && (e += a.style.paddingBottom); return e }, colorize: function (a, e) {
            var b = this.get("width"), c = this.get("radius"), h = this.get("height"), k = this.get("actx"), m = this.get("ctx"), i = 3 * c, c = this.get("premultiplyAlpha"), j = this.get("gradient"), n = this.get("opacity"), o = this.get("bounds"), q, l, p; null != a && null != e ? (a + i > b && (a = b - i), 0 > a && (a = 0), 0 > e && (e = 0), e + i > h && (e = h - i), q = a, b = e, l = a + i, h = e +
            i) : (q = 0 > o.l ? 0 : o.l, l = o.r > b ? b : o.r, b = 0 > o.t ? 0 : o.t, h = o.b > h ? h : o.b); k = k.getImageData(q, b, l - q, h - b); h = k.data; i = h.length; for (l = 3; l < i; l += 4) if (p = h[l], o = 4 * p) p = p < n ? p : n, h[l - 3] = j[o], h[l - 2] = j[o + 1], h[l - 1] = j[o + 2], c && (h[l - 3] /= 255 / p, h[l - 2] /= 255 / p, h[l - 1] /= 255 / p), h[l] = p; k.data = h; m.putImageData(k, q, b)
        }, drawAlpha: function (a, b, c, f) {
            var h = this.get("radius"), k = this.get("actx"); this.get("max"); var m = this.get("bounds"), i = a - 1.5 * h >> 0, j = b - 1.5 * h >> 0, n = a + 1.5 * h >> 0, o = b + 1.5 * h >> 0; k.shadowColor = "rgba(0,0,0," + (c ? c / this.store.max : "0.1") + ")";
            k.shadowOffsetX = 15E3; k.shadowOffsetY = 15E3; k.shadowBlur = 15; k.beginPath(); k.arc(a - 15E3, b - 15E3, h, 0, 2 * Math.PI, !0); k.closePath(); k.fill(); f ? this.colorize(i, j) : (i < m.l && (m.l = i), j < m.t && (m.t = j), n > m.r && (m.r = n), o > m.b && (m.b = o))
        }, toggleDisplay: function () { var a = this.get("visible"); this.get("canvas").style.display = a ? "none" : "block"; this.set("visible", !a) }, getImageData: function () { return this.get("canvas").toDataURL() }, clear: function () {
            var a = this.get("width"), b = this.get("height"); this.store.set("data", []); this.get("ctx").clearRect(0,
            0, a, b); this.get("actx").clearRect(0, 0, a, b)
        }, cleanup: function () { this.get("element").removeChild(this.get("canvas")) }
    }; n.h337 = n.heatmapFactory = { create: function (a) { return new b(a) }, util: { mousePosition: function (a) { var b, c; a.layerX ? (b = a.layerX, c = a.layerY) : a.offsetX && (b = a.offsetX, c = a.offsetY); if ("undefined" != typeof b) return [b, c] } } }
})(window); var BMapLib = window.BMapLib = BMapLib || {};
(function () {
    function n() { var c = document.createElement("canvas"); return !(!c.getContext || !c.getContext("2d")) } var j = BMapLib.HeatmapOverlay = function (c) { this.conf = c; this.heatmap = null; this.latlngs = []; this.bounds = null }; j.prototype = new BMap.Overlay; j.prototype.initialize = function (c) {
        this._map = c; var b = document.createElement("div"); b.style.position = "absolute"; b.style.top = 0; b.style.left = 0; b.style.border = 0; b.style.width = this._map.getSize().width + "px"; b.style.height = this._map.getSize().height + "px"; this.conf.element =
        b; if (!n()) return b; c.getPanes().mapPane.appendChild(b); this.heatmap = h337.create(this.conf); return this._div = b
    }; j.prototype.draw = function () {
        if (n()) {
            var c = this._map.getBounds(); if (!c.equals(this.bounds)) {
                this.bounds = c; var b = this._map.pointToOverlayPixel(c.getNorthEast()), a = this._map.pointToOverlayPixel(c.getSouthWest()), e = b.y, g = a.x, f = a.y - b.y, b = b.x - a.x; this.conf.element.style.left = g + "px"; this.conf.element.style.top = e + "px"; this.conf.element.style.width = b + "px"; this.conf.element.style.height = f + "px"; this.heatmap.store.get("heatmap").resize();
                if (0 < this.latlngs.length) { this.heatmap.clear(); f = this.latlngs.length; for (d = { max: this.heatmap.store.max, data: [] }; f--;) b = this.latlngs[f].latlng, c.containsPoint(b) && (b = this._map.pointToOverlayPixel(b), b = new BMap.Pixel(b.x - g, b.y - e), b = this.pixelTransform(b), d.data.push({ x: b.x, y: b.y, count: this.latlngs[f].c })); this.heatmap.store.setDataSet(d) }
            }
        }
    }; j.prototype.pixelTransform = function (c) {
        for (var b = this.heatmap.get("width"), a = this.heatmap.get("height") ; 0 > c.x;) c.x += b; for (; c.x > b;) c.x -= b; for (; 0 > c.y;) c.y += a; for (; c.y >
        a;) c.y -= a; c.x >>= 0; c.y >>= 0; return c
    }; j.prototype.setDataSet = function (c) {
        this.data = c; if (n()) {
            var b = this._map.getBounds(), a = { max: c.max, data: [] }, c = c.data, e = c.length; for (this.latlngs = []; e--;) {
                var g = new BMap.Point(c[e].lng, c[e].lat); if (b.containsPoint(g)) {
                    this.latlngs.push({ latlng: g, c: c[e].count }); var g = this._map.pointToOverlayPixel(g), f = this._map.pointToOverlayPixel(b.getSouthWest()).x, h = this._map.pointToOverlayPixel(b.getNorthEast()).y, g = new BMap.Pixel(g.x - f, g.y - h), g = this.pixelTransform(g); a.data.push({
                        x: g.x,
                        y: g.y, count: c[e].count
                    })
                }
            } this.heatmap.clear(); this.heatmap.store.setDataSet(a)
        }
    }; j.prototype.addDataPoint = function (c, b, a) { n() && (this.data && this.data.data && this.data.data.push({ lng: c, lat: b, count: a }), c = new BMap.Point(c, b), b = this.pixelTransform(this._map.pointToOverlayPixel(c)), this.heatmap.store.addDataPoint(b.x, b.y, a), this.latlngs.push({ latlng: c, c: a })) }; j.prototype.toggle = function () { n() && this.heatmap.toggleDisplay() }; j.prototype.setOptions = function (c) {
        if (n() && c) {
            for (var b in c) this.heatmap.set(b,
            c[b]), "gradient" == b ? this.heatmap.initColorPalette() : "opacity" == b && this.heatmap.set(b, parseInt(255 / (100 / c[b]), 10)); this.data && this.setDataSet(this.data)
        }
    }
})();